using GameFellowship.Data.Database;
using GameFellowship.Data.FormModels;

namespace GameFellowship.Data.Services;

public interface IPostService
{
    string DefaultConnectionSigns => "++";

    Task<bool> CreatePostAsync(PostModel model, int userID);
    Task<bool> DeletePostAsync(int postId);

    Task<bool> AddCurrentUserAsync(int postID, int userID);
	Task<bool> AddConversationAsync(ConversationModel model);

    Task<bool> DeleteCurrentUserAsync(int postID, int userID);

    Task<string[]> GetAudioPlatformsAsync(int count);
	Task<string[]> GetMatchTypesAsync(int count, string? gameName = null);
	Task<int[]> GetJoinedUserIds(int postId);
    Task<Conversation[]> GetConversations(int postId);
    Task<Post?> GetPostAsync(int postID);
	Task<Post[]> GetPostsAsync(IEnumerable<int> postIDs);
	Task<Post[]> GetPostsAsync(string gameName);
}