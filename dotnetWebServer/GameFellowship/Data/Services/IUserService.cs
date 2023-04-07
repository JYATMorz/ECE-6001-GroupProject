using GameFellowship.Data.FormModels;

namespace GameFellowship.Data.Services;

public interface IUserService
{
	string DefaultUserIconUri { get; }
	string DefaultUserIconFolder { get; }
	string DefaultUserName { get; }

	Task<bool> CreateNewUserAsync(UserModel model);

	Task<bool> AddNewLikedGameAsync(int userID, int gameID);
	Task<bool> AddNewJoinedPostAsync(int userID, int postID);
	Task<bool> AddNewCreatePostAsync(int userID, int postID);

	Task<bool> DeleteLikedGameAsync(int userID, int gameID);
	Task<bool> DeleteJoinedPostAsync(int userID, int postID);
	Task<bool> DeleteCreatePostAsync(int userID, int postID);

	Task<string> GetUserNameAsync(int userID);
	Task<string> GetUserIconURIAsync(int userID);
	Task<int[]> GetUserLikedGameIDsAsync(int userID);
	Task<int[]> GetUserJoinedPostIDsAsync(int userID);
	Task<(string, string)> GetUserNameIconPairAsync(int userID);
	Task<Dictionary<string, string>> GetUserGroupNameIconPairAsync(IEnumerable<int> userID);

    Task<(bool, int)> CheckUserPasswordAsync(string userName, string password);

    Task<bool> HasUserAsync(int userID);
	Task<bool> HasUserAsync(string userName);
	Task<bool> HasEmailAsync(string email);
}