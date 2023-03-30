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
        Task<IEnumerable<string>> GetGameNamesAsync(int count, string? prefix = null);
        Task<IEnumerable<string>> GetGameNamesAsync(IEnumerable<int> gameIDs);

        Task<bool> HasGameNameAsync(string name);
    }
}