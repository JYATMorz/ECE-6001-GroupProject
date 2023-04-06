namespace GameFellowship.Data.Services;

public class UserStatusService : IUserStatusService
{
	private readonly IUserService userService;
	private HashSet<int> loginUsers = new();

	public string SessionStorageKey { get; } = "userid";

	public UserStatusService(IUserService service)
	{
		userService = service;
	}

	public bool UserHasLogin(int userId)
	{
        return loginUsers.Contains(userId);
	}

	public (bool, int) UserLogin(string userName, string password)
	{
		var resultUser =
			from user in userService.Users
			where userName.Equals(user.UserName)
			select user;

		if (!resultUser.Any())
		{
			return (false, -1);
		}

		if (string.IsNullOrWhiteSpace(password))
		{
			return (false, -1);
		}
		else
		{
            // FIXME: Check the password
        }

		int userId = resultUser.First().UserID;
		if (!loginUsers.Add(userId))
		{
			return (false, -1);
		}

		return (true, userId);
	}

	public bool UserLogout(int userId)
	{
        return loginUsers.Remove(userId);
	}
}