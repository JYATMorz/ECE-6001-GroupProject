using GameFellowship.Data.FormModel;
using GameFellowship.Data.DtoModel;

namespace GameFellowship.Services;

public interface IUserService
{
    static string DefaultUserIconUri { get; } = "images/UserIcons/50913860_p9.jpg";
    static string DefaultUserIconFolder { get; } = "UserIcons";
    static string DefaultUserName { get; } = "匿名";

    Task<bool> CreateUserAsync(UserModel model);

    Task<bool> AddFollowedGameAsync(int userId, int gameId);
    Task<bool> AddJoinedPostAsync(int userId, int postId);
    Task<bool> AddCreatedPostAsync(int userId, int postId);

    Task<bool> DeleteFollowedGameAsync(int userId, int gameId);
    Task<bool> DeleteJoinedPostAsync(int userId, int postId);
    Task<bool> DeleteCreatedPostAsync(int userId, int postId);

    Task<bool> UpdateIconUri(int userId, string iconUri);
    Task<bool> UpdateEmail(int userId, string email);

    Task<string> GetUserNameAsync(int userId);
    Task<string> GetUserIconUriAsync(int userId);
    Task<string[]> GetUserFollowedGameNamesAsync(int userId);
    Task<PostDto[]> GetUserJoinedPostsAsync(int userId);
    Task<(string, string)> GetUserNameIconPairAsync(int userId);
    Task<Dictionary<string, string>> GetUserGroupNameIconPairAsync(IEnumerable<int> userId);
    Task<UserDto?> GetUserFullInfoAsync(int userId);

    Task<bool> HasUserAsync(int userId);
    Task<bool> HasUserAsync(string userName);
    Task<bool> HasEmailAsync(string email);
}