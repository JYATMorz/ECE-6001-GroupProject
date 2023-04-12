using GameFellowship.Data.FormModels;

namespace GameFellowship.Data.Services;

public interface IPostService
{
	Task<(bool, int)> CreateNewPostAsync(PostModel model, int userID);

	Task<bool> AddNewCurrentUserAsync(int postID, int userID);
	Task<bool> AddNewConversationAsync(int postID, ConversationTemp conversation);

    Task<bool> DeleteCurrentUserAsync(int postID, int userID);

    Task<string[]> GetAudioPlatformsAsync(int count);
	Task<string[]> GetMatchTypesAsync(int count, string? gameName = null);
	Task<PostTemp> GetPostAsync(int postID);
	Task<PostTemp[]> GetPostsAsync(IEnumerable<int> postIDs);
	Task<PostTemp[]> GetPostsAsync(string gameName);
}