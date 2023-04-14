using GameFellowship.Data.Database;
using GameFellowship.Data.FormModels;

namespace GameFellowship.Data.Services;

public interface IGameService
{
    string DefaultGameIconUri { get; }
	string DefaultGameIconFolder { get; }

	Task<bool> CreateNewGameAsync(GameModel model, int userId);

	Task<bool> UpdateLatestPostDate(string name);

	Task<Game[]> GetAllGameAsync();
	Task<Game?> GetGameAsync(int id);
	Task<Game?> GetGameAsync(string name);
	Task<string> GetGameIconAsync(string name);
	Task<string[]> GetGameNamesAsync(int count, string? prefix = null);
	Task<string[]> GetGameNamesAsync(IEnumerable<int> gameIDs);

	Task<bool> HasGameNameAsync(string name);
}