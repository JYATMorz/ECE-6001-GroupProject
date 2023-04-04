using GameFellowship.Data.FormModels;

namespace GameFellowship.Data.Services;

public interface IPostService
{
	Task<bool> CreateNewPostAsync(PostModel model, int userID);

	Task<bool> AddNewCurrentUserAsync(int postID, int userID);
	Task<bool> DeleteCurrentUserAsync(int postID, int userID);

	Task<bool> AddNewConversationAsync(int postID, Conversation conversation);

	Task<string[]> GetAudioPlatformsAsync(int count);
	Task<string[]> GetMatchTypesAsync(int count, string? gameName = null);
	Task<Post> GetPostAsync(int postID);
	Task<Post[]> GetPostsAsync(IEnumerable<int> postIDs);
	Task<Post[]> GetPostsAsync(string gameName);
}