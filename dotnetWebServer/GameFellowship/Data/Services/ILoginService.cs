namespace GameFellowship.Data.Services;

public interface ILoginService
{
    string LocalStorageKey { get; }

    Task<bool> UserHasLoginAsync(int userId);

    Task<(bool, int)> UserLoginAsync(string userName, string password);
	Task<bool> UserLogout(int userId);
}