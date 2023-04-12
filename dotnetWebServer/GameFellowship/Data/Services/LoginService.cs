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
		var resultUser = await dbContext.Users.Where(user => user.Id == userId).FirstOrDefaultAsync();
		if (resultUser is null || resultUser.LastLogin is null)
		{
			return false;
		}
		return true;
	}

	public async Task<(bool, int)> UserLoginAsync(string userName, string password)
	{
        using var dbContext = _dbContextFactory.CreateDbContext();
        var resultUser = await dbContext.Users.Where(user => userName == user.Name).FirstOrDefaultAsync();

		if (resultUser is null || resultUser.Password != password)
		{
			return (false, -1);
		}

		int userId = resultUser.Id;
		DateTime userLoginStamp = DateTime.Now;
        // TODO: Handle duplicate user login
        // resultUser.LastLogin ??= DateTime.Now;
        resultUser.LastLogin = userLoginStamp;
		
		await dbContext.SaveChangesAsync();

        return (true, userId);
	}

	public async Task<bool> UserLogout(int userId)
	{
        using var dbContext = _dbContextFactory.CreateDbContext();

		var resultUser = await dbContext.Users.Where(user => user.Id == userId).FirstOrDefaultAsync();
		if (resultUser is not null)
		{
			resultUser.LastLogin = null;
			await dbContext.SaveChangesAsync();

			return true;
		}

        return false;
	}
}