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

    public async Task<bool> UserHasLoginAsync(int userId)
	{
        using var dbContext = _dbContextFactory.CreateDbContext();
		// TODO: Should not only check nullable user datetime
		var resultUser = await dbContext.Users
                                        .Where(user => user.Id == userId)
                                        .FirstOrDefaultAsync();
		if (resultUser is null || resultUser.LastLogin is null)
		{
			return false;
		}

		return true;
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
        // TODO: Handle duplicate user login
        // resultUser.LastLogin ??= DateTime.Now;
        resultUser.LastLogin = userLoginStamp;
		
		await dbContext.SaveChangesAsync();

        return (true, $"{userId}++{userLoginStamp:O}");
	}

	public async Task<bool> UserLogout(string? userLoginInfo)
    {
        if (string.IsNullOrEmpty(userLoginInfo))
        {
            return false;
        }

        string[] userInfo = userLoginInfo.Trim().Split("++");
        if (userInfo.Length != 2)
        {
            return false;
        }
        if (!int.TryParse(userInfo[0], out int userId) ||
            !DateTime.TryParse(userInfo[1], out DateTime loginTime))
        {
            // TODO: Pay Attention
            Console.WriteLine(userLoginInfo);
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

        resultUser.LastLogin = null;
        await dbContext.SaveChangesAsync();

        return true;
    }
}