using GameFellowship.Data.Database;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace GameFellowship.Data.Services;

public class LoginService : ILoginService
{
	public static string LocalStorageKey => "user";

    private readonly string _defaultConnectionSign = "++";

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

        // Not login longer than 3 days
        if (userLogin.AddDays(3) < DateTime.Now)
        {
            return (false, -1);
        }

        using var dbContext = _dbContextFactory.CreateDbContext();
		var resultUser = await dbContext.Users
                                        .Where(user => user.Id == userId)
                                        .AnyAsync(user => user.LastLogin == userLogin);

        return resultUser ? (true, userId) : (false, -1);
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

        return true;
    }

    private bool TryGetUserInfo(string? userLoginInfo, out int userId, out DateTime userLogin)
    {
        userId = -1;
        userLogin = DateTime.MinValue;

        if (string.IsNullOrWhiteSpace(userLoginInfo))
        {
            return false;
        }

        string[] userInfo = userLoginInfo.Trim().Split(_defaultConnectionSign);
        if (userInfo.Length != 2)
        {
            return false;
        }

        if (!int.TryParse(userInfo[0], out userId) ||
            !DateTime.TryParse(userInfo[1], DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal,
                               out userLogin))
        {
            return false;
        }

        return true;
    }
}