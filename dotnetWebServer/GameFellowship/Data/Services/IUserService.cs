using GameFellowship.Data.FormModels;

namespace GameFellowship.Data.Services;

public interface IUserService
{
	public List<User> Users { get; }

	public string DefaultUserIconURI { get; }
	public string DefaultUserName { get; }

	Task<bool> CreateNewUserAsync(UserModel user);

	Task<string> GetUserNameAsync(int userID);
	Task<string> GetUserIconURIAsync(int userID);
	Task<int[]> GetUserLikedGameIDsAsync(int userID);
	Task<int[]> GetUserJoinedPostIDsAsync(int userID);
	Task<(string, string)> GetUserNameIconPairAsync(int userID);
	Task<Dictionary<string, string>> GetUserGroupNameIconPairAsync(IEnumerable<int> userID);

	Task<bool> HasUserAsync(int userID);
	Task<bool> HasUserAsync(string userName);
	Task<bool> HasEmailAsync(string email);
}