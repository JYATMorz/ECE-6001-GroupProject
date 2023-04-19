using GameFellowship.Data.FormModel;
using GameFellowship.Data.DtoModel;
using GameFellowship.Data.Database;
using Microsoft.EntityFrameworkCore;

namespace GameFellowship.Services;

public class GameService : IGameService
{
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
                LastPostDate = DateTime.Now.ToUniversalTime(),
                FollowingUsers = new List<User> { resultUser }
            };
        }
        else
        {
            newGame = new()
            {
                Name = model.GameName,
                IconURI = model.IconURI,
                LastPostDate = DateTime.Now.ToUniversalTime(),
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
                                        .Where(game => game.Name.ToLower() == name.ToLower())
                                        .FirstOrDefaultAsync();
        if (resultGame is null) return false;

        resultGame.LastPostDate = DateTime.Now.ToUniversalTime();
        await dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<GameDto[]> GetAllGameAsync()
    {
        using var dbContext = _dbContextFactory.CreateDbContext();
        Game[] resultGames = await dbContext.Games
                                            .Include(game => game.Posts).AsSplitQuery()
                                            .Include(game => game.FollowingUsers).AsSplitQuery()
                                            .ToArrayAsync();

        if(resultGames.Length == 0) return Array.Empty<GameDto>();

        return Array.ConvertAll(resultGames, game => new GameDto(game));
    }

    public async Task<GameDto?> GetGameAsync(int gameId)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();
        var resultGame = await dbContext.Games
                                        .Where(game => game.Id == gameId)
                                        .Include(game => game.FollowingUsers).AsSplitQuery()
                                        .Include(game => game.Posts).AsSplitQuery()
                                        .FirstOrDefaultAsync();

        if (resultGame is null) return null;

        return new GameDto(resultGame);
    }

    public async Task<GameDto?> GetGameAsync(string name)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();
        var resultGame = await dbContext.Games
                                        .Where(game => game.Name.ToLower() == name.ToLower())
                                        .Include(game => game.Posts).AsSplitQuery()
                                        .Include(game => game.FollowingUsers).AsSplitQuery()
                                        .FirstOrDefaultAsync();

        if (resultGame is null) return null;

        return new GameDto(resultGame);
    }

    public async Task<string> GetGameIconAsync(string name)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();
        var resultGame = await dbContext.Games
                                        .Where(game => game.Name.ToLower() == name.ToLower())
                                        .Select(game => game.IconURI)
                                        .FirstOrDefaultAsync();

        return string.IsNullOrWhiteSpace(resultGame) ? IGameService.DefaultGameIconUri : resultGame;
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

        return await dbContext.Games.AnyAsync(game => game.Name.ToLower() == name.ToLower());
    }
}