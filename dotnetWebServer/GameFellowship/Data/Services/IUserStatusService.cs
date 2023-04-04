﻿namespace GameFellowship.Data.Services;

public interface IUserStatusService
{
	int LoginUserID { get; }
	bool UserHasLogin { get; }

	Task<bool> UserLoginAsync(string userName, string password);
	Task<bool> UserLogoutAsync();
}