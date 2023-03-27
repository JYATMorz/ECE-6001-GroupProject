namespace GameFellowship.Data
{
    public interface IPostService
    {
        Task<string[]> GetAudioPlatformsAsync(int num);
        Task<string[]> GetMatchTypesAsync(int num, string? game = null);
        Task<Post> GetPostAsync(int id);
        Task<Post[]> GetPostsAsync(int[] postIDs);
        Task<Post[]> GetPostsAsync(string game);
    }
}