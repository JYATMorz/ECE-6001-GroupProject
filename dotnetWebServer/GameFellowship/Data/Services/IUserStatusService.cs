namespace GameFellowship.Data.Services;

public interface IUserStatusService
{
    string SessionStorageKey { get; }

    bool UserHasLogin(int userId);
    (bool, int) UserLogin(string userName, string password);
	bool UserLogout(int userId);
}