namespace GameFellowship.Data.Services;

public interface IUserStatusService
{
    string SessionStorageKey { get; }

    bool UserHasLogin(int userId);

    Task<(bool, int)> UserLoginAsync(string userName, string password);
	bool UserLogout(int userId);
}