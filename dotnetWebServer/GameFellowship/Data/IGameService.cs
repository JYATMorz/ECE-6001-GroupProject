namespace GameFellowship.Data
{
    public interface IGameService
    {
        Task<bool> CreateNewGameAsync(GameModel model);

        Task<bool> UpdateNewLatestPostDate(string name);

        Task<Game[]> GetAllGameAsync();
        Task<Game> GetGameAsync(int id);
        Task<Game> GetGameAsync(string name);
        Task<string> GetGameIconAsync(string name);
        Task<string[]> GetGameNamesAsync(int count, string? prefix = null);
        Task<string[]> GetGameNamesAsync(IEnumerable<int> gameIDs);

        Task<bool> HasGameNameAsync(string name);
    }
}