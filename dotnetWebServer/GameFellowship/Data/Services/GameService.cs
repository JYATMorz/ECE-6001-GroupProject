using GameFellowship.Data.FormModels;

namespace GameFellowship.Data.Services;

public class GameService : IGameService
{
	private List<Game> _games = new() {
		new Game ("Destiny 2", 114, new DateTime(2023,3,11,11,11,11)),
		new Game ("Touhou Project", 514, new DateTime(2023,3,20,20,20,20), "images/GameIcons/75750856_p0.jpg"),
		new Game ("Minecraft", 1919, new DateTime(2023,3,22,22,22,22), "images/GameIcons/75750856_p0.jpg"),
		new Game ("APEX", 8100),
		new Game ("Destiny 1", 11, new DateTime(2023,3,11,11,11,11), "images/GameIcons/75750856_p0.jpg"),
		new Game ("MineCity", 2020, new DateTime(2023,3,22,22,22,22)),
		new Game ("CS:GO", 77777),
		new Game ("CS 2", 888, new DateTime(2023,3,23,23,23,23), "images/UserIcons/50913860_p9.jpg")
	};

	public string DefaultGameIconUri { get; } = "images/GameIcons/gametitle.jpg";
	public string DefaultGameIconFolder { get; } = "GameIcons";

	public Task<(bool, int)> CreateNewGameAsync(GameModel model)
	{
		Game newGame = new(model);
		_games.Add(newGame);

		// TODO: IF everything goes well
		return Task.FromResult((true, newGame.GameID));
	}

	public Task<bool> UpdateNewLatestPostDate(string name)
	{
		var selectedGame =
			from game in _games
			where game.GameName.ToLower() == name.ToLower()
			select game;

		if (!selectedGame.Any())
		{
			return Task.FromResult(false);
		}

		selectedGame.First().LastPostDate = DateTime.Now;
		// TODO: Save it back to database

		return Task.FromResult(true);
	}

	public Task<Game[]> GetAllGameAsync()
	{
		return Task.FromResult(_games.ToArray());
	}

	public Task<Game> GetGameAsync(int id)
	{
		var resultGame =
			from game in _games
			where game.GameID == id
			select game;

		if (!resultGame.Any())
			return Task.FromResult(new Game());

		return Task.FromResult(resultGame.First());
	}

	public Task<Game> GetGameAsync(string name)
	{
		var resultGame =
			from game in _games
			where game.GameName.ToLower() == name.ToLower()
			select game;

		if (!resultGame.Any())
			return Task.FromResult(new Game());

		return Task.FromResult(resultGame.First());
	}

	public Task<string> GetGameIconAsync(string name)
	{
		var resultGameIcon =
			from game in _games
			where game.GameName.ToLower() == name.ToLower()
			select game.IconURI;

		if (!resultGameIcon.Any())
			return Task.FromResult(DefaultGameIconUri);

		return Task.FromResult(resultGameIcon.First());
	}

	public Task<string[]> GetGameNamesAsync(IEnumerable<int> gameIDs)
	{
		var resultGames =
			from game in _games
			where gameIDs.Contains(game.GameID)
			select game.GameName;

		if (!resultGames.Any())
		{
			return Task.FromResult(Array.Empty<string>());
		}

		return Task.FromResult(resultGames.ToArray());
	}

	public Task<string[]> GetGameNamesAsync(int count, string? prefix = null)
	{
		IEnumerable<string> resultGames;

		if (string.IsNullOrWhiteSpace(prefix))
		{
			resultGames = (
				from game in _games
				orderby game.Followers descending
				select game.GameName
			).Take(count);
		}
		else
		{
			resultGames = (
				from game in _games
				where game.GameName.Contains(prefix)
				orderby game.Followers descending
				select game.GameName
			).Take(count);
		}

		if (!resultGames.Any())
		{
			return Task.FromResult(Array.Empty<string>());
		}

		return Task.FromResult(resultGames.ToArray());
	}

	public Task<bool> HasGameNameAsync(string name)
	{
		var anyGameName =
			from game in _games
			where game.GameName.ToLower() == name.ToLower()
			select game.GameName
		;

		return Task.FromResult(anyGameName.Any());
	}
}