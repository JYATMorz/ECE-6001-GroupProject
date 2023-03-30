namespace GameFellowship.Data
{
    public interface IPostService
    {
        Task<bool> CreateNewPostAsync(PostModel model, int userID);

        Task<bool> AddNewCurrentUserAsync(int postID, int userID);
        Task<bool> DeleteCurrentUserAsync(int postID, int userID);

        Task<bool> AddNewConversationAsync(int postID, Conversation conversation);

        Task<IEnumerable<string>> GetAudioPlatformsAsync(int count);
        Task<IEnumerable<string>> GetMatchTypesAsync(int count, string? gameName = null);
        Task<Post> GetPostAsync(int postID);
        Task<IEnumerable<Post>> GetPostsAsync(IEnumerable<int> postIDs);
        Task<IEnumerable<Post>> GetPostsAsync(string gameName);
    }
}