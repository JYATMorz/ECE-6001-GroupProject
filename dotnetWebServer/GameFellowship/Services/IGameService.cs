using GameFellowship.Data.FormModel;
using GameFellowship.Data.DtoModel;

namespace GameFellowship.Services;

public interface IGameService
{
    static string DefaultGameIconUri { get; } = "images/GameIcons/default.jpg";
    static string DefaultGameIconFolder { get; } = "GameIcons";

    Task<bool> CreateNewGameAsync(GameModel model, int userId);

    Task<bool> UpdateLatestPostDate(string name);

    Task<GameDto[]> GetAllGameAsync();
    Task<GameDto?> GetGameAsync(int id);
    Task<GameDto?> GetGameAsync(string name);
    Task<string> GetGameIconAsync(string name);
    Task<string[]> GetGameNamesAsync(int count, string? prefix = null);
    Task<string[]> GetGameNamesAsync(IEnumerable<int> gameIDs);

    Task<bool> HasGameNameAsync(string name);
}