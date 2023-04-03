namespace GameFellowship.Data
{
	public class UserStatus : IUserStatus
	{
		private readonly IUserService userService;

		// FIXME: Still for one user only
		// TODO: Local Session Storage to seperate user
		// https://www.cnblogs.com/towerbit/p/15044935.html
		public int LoginUserID { get; private set; } = -1;
		public bool UserHasLogin => LoginUserID > 0;

		public UserStatus(IUserService service)
		{
			userService = service;
		}

		public Task<bool> UserLoginAsync(string userName, string password)
		{
			var resultUser =
				from user in userService.Users
				where userName.Equals(user.UserName)
				select user;

			if (!resultUser.Any())
			{
				return Task.FromResult(false);
			}

			if (LoginUserID == resultUser.First().UserID)
			{
				return Task.FromResult(false);
			}

			// FIXME: Check the password
			if (string.IsNullOrWhiteSpace(password))
			{
				return Task.FromResult(false);
			}

			LoginUserID = resultUser.First().UserID;

			return Task.FromResult(true);
		}

		public Task<bool> UserLogoutAsync()
		{
			if (userService.Users.Any(user => user.UserID == LoginUserID))
			{
				LoginUserID = -1;
				return Task.FromResult(true);
			}

			return Task.FromResult(false);
		}
	}
}
