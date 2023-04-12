using GameFellowship.Data.FormModels;

namespace GameFellowship.Data.Services;

public interface IUserService
{
	string DefaultUserIconUri { get; }
	string DefaultUserIconFolder { get; }
	string DefaultUserName { get; }

	Task<bool> CreateNewUserAsync(UserModel model);

	Task<bool> AddNewLikedGameAsync(int userId, int gameId);
	Task<bool> AddNewJoinedPostAsync(int userId, int postId);
	Task<bool> AddNewCreatePostAsync(int userId, int postId);

	Task<bool> DeleteLikedGameAsync(int userId, int gameId);
	Task<bool> DeleteJoinedPostAsync(int userId, int postId);
	Task<bool> DeleteCreatePostAsync(int userId, int postId);

	Task<string> GetUserNameAsync(int userId);
	Task<string> GetUserIconUriAsync(int userId);
	Task<int[]> GetUserLikedGameIdsAsync(int userId);
	Task<int[]> GetUserJoinedPostIdsAsync(int userId);
	Task<(string, string)> GetUserNameIconPairAsync(int userId);
	Task<Dictionary<string, string>> GetUserGroupNameIconPairAsync(IEnumerable<int> userId);

    Task<bool> HasUserAsync(int userId);
	Task<bool> HasUserAsync(string userName);
	Task<bool> HasEmailAsync(string email);
}