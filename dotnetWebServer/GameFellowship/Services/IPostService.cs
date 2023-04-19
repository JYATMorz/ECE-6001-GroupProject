using GameFellowship.Data.DtoModel;
using GameFellowship.Data.FormModel;

namespace GameFellowship.Services;

public interface IPostService
{
    static string DefaultConnectionSigns => "++";

    Task<bool> CreatePostAsync(PostModel model, int userID);
    Task<bool> DeletePostAsync(int postId);

    Task<bool> AddCurrentUserAsync(int postID, int userID);
    Task<bool> AddConversationAsync(ConversationModel model);

    Task<bool> DeleteCurrentUserAsync(int postID, int userID);

    Task<string[]> GetAudioPlatformsAsync(int count);
    Task<string[]> GetMatchTypesAsync(int count, string? gameName = null);
    Task<int[]> GetJoinedUserIds(int postId);
    Task<ConversationDto[]> GetConversations(int postId);
    Task<PostDto?> GetPostAsync(int postID);
    Task<PostDto[]> GetPostsAsync(IEnumerable<int> postIDs);
    Task<PostDto[]> GetPostsAsync(string gameName);
}