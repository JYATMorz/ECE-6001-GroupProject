using GameFellowship.Data.FormModels;
using GameFellowship.Data.Database;

namespace GameFellowship.Data.Services;

public interface IUserService
{
	string DefaultUserIconUri { get; }
	string DefaultUserIconFolder { get; }
	string DefaultUserName { get; }

	Task<bool> CreateNewUserAsync(UserModel model);

	Task<bool> AddFollowedGameAsync(int userId, int gameId);
	Task<bool> AddJoinedPostAsync(int userId, int postId);
	// Task<bool> AddCreatedPostAsync(int userId, int postId);

	Task<bool> DeleteFollowedGameAsync(int userId, int gameId);
	Task<bool> DeleteJoinedPostAsync(int userId, int postId);
	Task<bool> DeleteCreatedPostAsync(int userId, int postId);

	Task<string> GetUserNameAsync(int userId);
	Task<string> GetUserIconUriAsync(int userId);
	Task<string[]> GetUserFollowedGameNamesAsync(int userId);
	Task<Post[]> GetUserJoinedPostsAsync(int userId);
	Task<(string, string)> GetUserNameIconPairAsync(int userId);
	Task<Dictionary<string, string>> GetUserGroupNameIconPairAsync(IEnumerable<int> userId);

    Task<bool> HasUserAsync(int userId);
	Task<bool> HasUserAsync(string userName);
	Task<bool> HasEmailAsync(string email);
}