namespace GameFellowship.Data
{
    public interface IUserService
    {
        public int LoginUserID { get; }
        public bool UserHasLogin { get; }

        public string DefaultUserIconURI { get; }
        public string DefaultUserName { get; }

        Task<string> GetUserNameAsync(int userID);
        Task<string> GetUserIconURIAsync(int userID);
        Task<int[]> GetUserLikedGameIDsAsync(int userID);
        Task<int[]> GetUserJoinedPostIDsAsync(int userID);
        Task<int[]> GetLoginUserJoinedPostIDsAsync();
        Task<(string, string)> GetUserNameIconPairAsync(int userID);
        Task<Dictionary<string, string>> GetUserGroupNameIconPairAsync(IEnumerable<int> userID);

        Task<bool> HasUserAsync(int userID);
		Task<bool> HasUserAsync(string userName);
		Task<bool> HasEmailAsync(string email);

		Task<bool> UserLoginAsync(string userName, string password);
        Task<bool> UserLogoutAsync();
    }
}