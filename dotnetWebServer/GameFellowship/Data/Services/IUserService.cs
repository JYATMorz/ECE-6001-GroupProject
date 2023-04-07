using GameFellowship.Data.FormModels;

namespace GameFellowship.Data.Services;

public interface IUserService
{
	string DefaultUserIconUri { get; }
	string DefaultUserIconFolder { get; }
	string DefaultUserName { get; }

	Task<bool> CreateNewUserAsync(UserModel user);

	Task<bool> AddNewLikedGame(int userID, int GameID);

	Task<string> GetUserNameAsync(int userID);
	Task<string> GetUserIconURIAsync(int userID);
	Task<int[]> GetUserLikedGameIDsAsync(int userID);
	Task<int[]> GetUserJoinedPostIDsAsync(int userID);
	Task<(string, string)> GetUserNameIconPairAsync(int userID);
	Task<Dictionary<string, string>> GetUserGroupNameIconPairAsync(IEnumerable<int> userID);

    Task<(bool, int)> CheckUserPassword(string userName, string password);

    Task<bool> HasUserAsync(int userID);
	Task<bool> HasUserAsync(string userName);
	Task<bool> HasEmailAsync(string email);
}