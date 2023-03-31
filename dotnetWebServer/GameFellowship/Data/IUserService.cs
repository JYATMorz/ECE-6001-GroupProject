namespace GameFellowship.Data
{
    public interface IUserService
    {
        // TODO: Try to integrate into one function
        Task<string?> GetUserNameAsync(int userID);
        Task<string?> GetUserIconURIAsync(int userID);
        Task<int[]> GetUserLikedGameIDsAsync(int userID);
        Task<int[]> GetUserJoinedPostIDsAsync(int userID);
        Task<(string, string)> GetUserNameIconPairAsync(int userID);
        Task<Dictionary<string, string>> GetUserGroupNameIconPairAsync(IEnumerable<int> userID);

        Task<bool> HasUserAsync(int userID);
    }
}