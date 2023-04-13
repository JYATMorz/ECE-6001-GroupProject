using GameFellowship.Data.Database;
using Microsoft.EntityFrameworkCore;

namespace GameFellowship.Data.Services;

public class LoginService : ILoginService
{
	public string LocalStorageKey { get; } = "user";

    private readonly IDbContextFactory<GameFellowshipDb> _dbContextFactory;

    public LoginService(IDbContextFactory<GameFellowshipDb> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<(bool, int)> UserHasLoginAsync(string? userLoginInfo)
	{
        if (!TryGetUserInfo(userLoginInfo, out int userId, out DateTime userLogin))
        {
            return (false, -1);
        }

        using var dbContext = _dbContextFactory.CreateDbContext();
		var resultUser = await dbContext.Users
                                        .Where(user => user.Id == userId)
                                        .FirstOrDefaultAsync();
        // Never Login || Logged out before || Logged in elsewhere
		if (resultUser is null || resultUser.LastLogin != userLogin)
		{
			return (false, -1);
		}

		return (true, userId);
	}

	public async Task<(bool, string)> UserLoginAsync(string userName, string password)
	{
        using var dbContext = _dbContextFactory.CreateDbContext();
        var resultUser = await dbContext.Users
                                        .Where(user => userName == user.Name)
                                        .FirstOrDefaultAsync();

		if (resultUser is null || resultUser.Password != password)
		{
			return (false, string.Empty);
		}

		int userId = resultUser.Id;
		DateTime userLoginStamp = DateTime.Now.ToUniversalTime();

        resultUser.LastLogin = userLoginStamp;
        await dbContext.SaveChangesAsync();

        return (true, $"{userId}++{userLoginStamp:O}");
	}

	public async Task<bool> UserLogoutAsync(string? userLoginInfo)
    {
        if (!TryGetUserInfo(userLoginInfo, out int userId, out DateTime userLogin))
        {
            return false;
        }

        using var dbContext = _dbContextFactory.CreateDbContext();
        var resultUser = await dbContext.Users
                                        .Where(user => user.Id == userId)
                                        .FirstOrDefaultAsync();
        if (resultUser is null)
        {
            return false;
        }
        // Same Account and Same session/local storage
        if (resultUser.LastLogin == userLogin)
        {
            resultUser.LastLogin = DateTime.Now.ToUniversalTime();
            await dbContext.SaveChangesAsync();
        }

        // TODO: If logins table exists

        return true;
    }

    private static bool TryGetUserInfo(string? userLoginInfo, out int userId, out DateTime userLogin)
    {
        userId = -1;
        userLogin = DateTime.MinValue;
        if (string.IsNullOrWhiteSpace(userLoginInfo))
        {
            // TODO: Pay Attention
            Console.WriteLine(userLoginInfo);
            return false;
        }

        string[] userInfo = userLoginInfo.Trim().Split("++");
        if (userInfo.Length != 2)
        {
            // TODO: Pay Attention
            Console.WriteLine(userLoginInfo);
            return false;
        }
        if (!int.TryParse(userInfo[0], out userId) ||
            !DateTime.TryParse(userInfo[1], out userLogin))
        {
            // TODO: Pay Attention
            Console.WriteLine(userLoginInfo);
            return false;
        }

        return true;
    }
}