using GameFellowship.Data.Database;
using GameFellowship.Data.FormModels;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace GameFellowship.Data.Services;

public class GameService : IGameService
{
	public string DefaultGameIconUri { get; } = "images/GameIcons/gametitle.jpg";
	public string DefaultGameIconFolder { get; } = "GameIcons";

    private readonly IDbContextFactory<GameFellowshipDb> _dbContextFactory;

    public GameService(IDbContextFactory<GameFellowshipDb> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<bool> CreateNewGameAsync(GameModel model, int userId)
	{
		if (await HasGameNameAsync(model.GameName)) return false;

        using var dbContext = _dbContextFactory.CreateDbContext();

		Game newGame;
        if (model.Follow)
		{
			var resultUser = await dbContext.Users
											.Where(user => user.Id == userId)
											.FirstOrDefaultAsync();
			if (resultUser is null) return false;

            newGame = new()
            {
                Name = model.GameName,
                IconURI = model.IconURI,
                LastPostDate = DateTime.Now,
                Followers = 1,
                FollowingUsers = new List<User> { resultUser }
            };
        }
		else
		{
            newGame = new()
            {
                Name = model.GameName,
                IconURI = model.IconURI,
                LastPostDate = DateTime.Now,
                Followers = 0
            };
        }

		dbContext.Games.Add(newGame);
		await dbContext.SaveChangesAsync();

        return true;
	}

	public async Task<bool> UpdateLatestPostDate(string name)
	{
        using var dbContext = _dbContextFactory.CreateDbContext();
        var resultGame = await dbContext.Games
                                        .Where(game => game.Name == name)
                                        .FirstOrDefaultAsync();
		if (resultGame is null) return false;

		resultGame.LastPostDate = DateTime.Now.ToUniversalTime();
		await dbContext.SaveChangesAsync();

		return true;
    }

	public async Task<Game[]> GetAllGameAsync()
	{
        using var dbContext = _dbContextFactory.CreateDbContext();

		return await dbContext.Games.ToArrayAsync();
    }

	public async Task<Game?> GetGameAsync(int gameId)
	{
        using var dbContext = _dbContextFactory.CreateDbContext();
		var resultGame = await dbContext.Games
                                        .Where(game => game.Id == gameId)
                                        .Include(game => game.FollowingUsers)
                                        .FirstOrDefaultAsync();

        if (resultGame is not null && resultGame.FollowingUsers.Count != resultGame.Followers)
        {
            resultGame.Followers = resultGame.FollowingUsers.Count;
            await dbContext.SaveChangesAsync();
        }

        return resultGame;
    }

	public async Task<Game?> GetGameAsync(string name)
	{
        using var dbContext = _dbContextFactory.CreateDbContext();
		var resultGame = await dbContext.Games
										.Where(game => game.Name == name)
										.Include(game => game.FollowingUsers)
										.FirstOrDefaultAsync();

		if (resultGame is not null && resultGame.FollowingUsers.Count != resultGame.Followers)
		{
			resultGame.Followers = resultGame.FollowingUsers.Count;
			await dbContext.SaveChangesAsync();
        }

		return resultGame;
    }

	public async Task<string> GetGameIconAsync(string name)
	{
        using var dbContext = _dbContextFactory.CreateDbContext();
		var resultGame = await dbContext.Games
										.Where(game => game.Name == name)
										.Select(game => game.IconURI)
										.FirstOrDefaultAsync();

		return string.IsNullOrWhiteSpace(resultGame) ? DefaultGameIconUri : resultGame;
    }

	public async Task<string[]> GetGameNamesAsync(IEnumerable<int> gameIDs)
	{
        using var dbContext = _dbContextFactory.CreateDbContext();
		var resultGame = await dbContext.Games
										.Where(game => gameIDs.Contains(game.Id))
										.Select(game => game.Name)
										.ToArrayAsync();

		return resultGame;
    }

	public async Task<string[]> GetGameNamesAsync(int count, string? prefix = null)
	{
		using var dbContext = _dbContextFactory.CreateDbContext();
		string[] resultGame;

		if (string.IsNullOrWhiteSpace(prefix))
		{
            resultGame = await dbContext.Games
                                        .Select(game => game.Name)
										.Take(count)
                                        .ToArrayAsync();
        }
		else
		{
            resultGame = await dbContext.Games
                                        .Where(game => game.Name.Contains(prefix))
                                        .Select(game => game.Name)
                                        .Take(count)
                                        .ToArrayAsync();
        }

		return resultGame;
    }

    public async Task<bool> HasGameNameAsync(string name)
	{
        using var dbContext = _dbContextFactory.CreateDbContext();

		return await dbContext.Games.AnyAsync(game => game.Name == name);
    }
}