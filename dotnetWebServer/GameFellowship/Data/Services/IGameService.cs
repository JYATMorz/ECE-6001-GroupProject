using GameFellowship.Data.FormModels;

namespace GameFellowship.Data.Services;

public interface IGameService
{
    string DefaultGameIconUri { get; }
	string DefaultGameIconFolder { get; }

	Task<(bool, int)> CreateNewGameAsync(GameModel model);

	Task<bool> UpdateNewLatestPostDate(string name);

	Task<GameTemp[]> GetAllGameAsync();
	Task<GameTemp> GetGameAsync(int id);
	Task<GameTemp> GetGameAsync(string name);
	Task<string> GetGameIconAsync(string name);
	Task<string[]> GetGameNamesAsync(int count, string? prefix = null);
	Task<string[]> GetGameNamesAsync(IEnumerable<int> gameIDs);

	Task<bool> HasGameNameAsync(string name);
}