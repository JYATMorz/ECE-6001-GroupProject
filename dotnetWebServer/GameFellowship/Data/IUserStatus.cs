namespace GameFellowship.Data
{
	public interface IUserStatus
	{
		int LoginUserID { get; }
		bool UserHasLogin { get; }

		Task<bool> UserLoginAsync(string userName, string password);
		Task<bool> UserLogoutAsync();
	}
}