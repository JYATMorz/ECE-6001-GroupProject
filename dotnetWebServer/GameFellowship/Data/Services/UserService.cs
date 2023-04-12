using GameFellowship.Data.Database;
using GameFellowship.Data.FormModels;
using Microsoft.EntityFrameworkCore;

namespace GameFellowship.Data.Services;

public class UserService : IUserService
{
	private List<UserTemp> _users = new() {
		new UserTemp("User 1", null, null, new int[]{1,2,3}, new int[]{1,2}, new int[]{1,2}, null),
		new UserTemp("User 2 with long", null, "images/GameIcons/75750856_p0.jpg", null, null, new int[]{1,2}, null),
		new UserTemp("User 3", null, null, null, null, new int[]{1,2}, null),
		new UserTemp("User 4", null, null, null, new int[]{3}, new int[]{3}, null),
		new UserTemp("User 55555555555", null, null, null, null, new int[]{1,2,3}, null),
		new UserTemp("User 6", null, "images/GameIcons/75750856_p0.jpg", null, null, new int[]{1}, null),
	};

	public string DefaultUserIconUri { get; } = "images/UserIcons/50913860_p9.jpg";
	public string DefaultUserIconFolder { get; } = "UserIcons";
	public string DefaultUserName { get; } = "匿名";

	private readonly IDbContextFactory<GameFellowshipDb> _dbContextFactory;

	public UserService(IDbContextFactory<GameFellowshipDb> dbContextFactory)
	{
		_dbContextFactory = dbContextFactory;
	}

	public async Task<bool> CreateNewUserAsync(UserModel model)
	{
		if (await HasUserAsync(model.UserName))
		{
			return false;
		}
		if (!string.IsNullOrWhiteSpace(model.UserEmail) && await HasEmailAsync(model.UserEmail))
		{
			return false;
		}

		using var dbContext = _dbContextFactory.CreateDbContext();
		var user = new User
		{
			Password = model.UserPassword,
			Email = model.UserEmail,
			IconURI = model.UserIconURI
		};

		if (dbContext is null)
		{
			return false;
		}
        dbContext.Users.Add(user);
		await dbContext.SaveChangesAsync();

		// TODO: Old Model in memory
        UserTemp newUser = new(model);
		_users.Add(newUser);

		return true;
	}

	public Task<bool> AddNewLikedGameAsync(int userID, int gameID)
	{
        var resultUser =
            from user in _users
            where user.UserID == userID
            select user;

        if (resultUser is null || !resultUser.Any())
        {
			return Task.FromResult(false);
        }

        resultUser.First().LikedGameIDs.Add(gameID);
        return Task.FromResult(true);
    }

    public Task<bool> AddNewJoinedPostAsync(int userID, int postID)
	{
        var resultUser =
            from user in _users
            where user.UserID == userID
            select user;

        if (resultUser is null || !resultUser.Any())
        {
            return Task.FromResult(false);
        }

        resultUser.First().JoinedPostIDs.Add(postID);
        return Task.FromResult(true);
    }

    public Task<bool> AddNewCreatePostAsync(int userID, int postID)
	{
        var resultUser =
            from user in _users
            where user.UserID == userID
            select user;

        if (resultUser is null || !resultUser.Any())
        {
            return Task.FromResult(false);
        }

		resultUser.First().CreatedPostIDs.Add(postID);
        resultUser.First().JoinedPostIDs.Add(postID);
        return Task.FromResult(true);
    }

    public Task<bool> DeleteLikedGameAsync(int userID, int gameID)
	{
        var resultUser =
            from user in _users
            where user.UserID == userID
            select user;

        if (resultUser is null || !resultUser.Any())
        {
            return Task.FromResult(false);
        }

        return Task.FromResult(resultUser.First().LikedGameIDs.Remove(gameID));
    }

    public Task<bool> DeleteJoinedPostAsync(int userID, int postID)
	{
        var resultUser =
            from user in _users
            where user.UserID == userID
            select user;

        if (resultUser is null || !resultUser.Any())
        {
            return Task.FromResult(false);
        }

        return Task.FromResult(resultUser.First().JoinedPostIDs.Remove(postID));
    }

    public Task<bool> DeleteCreatePostAsync(int userID, int postID)
	{
        var resultUser =
            from user in _users
            where user.UserID == userID
            select user;

        if (resultUser is null || !resultUser.Any())
        {
            return Task.FromResult(false);
        }

		resultUser.First().JoinedPostIDs.Remove(postID);

        return Task.FromResult(resultUser.First().CreatedPostIDs.Remove(postID));
    }

    public Task<string> GetUserNameAsync(int userID)
	{
		var resultUser =
			from user in _users
			where user.UserID == userID
			select user;

		if (resultUser is not null && resultUser.Any())
		{
			return Task.FromResult(resultUser.First().UserName);
		}
		else
		{
			return Task.FromResult(DefaultUserName);
		}
	}

	public Task<string> GetUserIconURIAsync(int userID)
	{
		var resultUser =
			from user in _users
			where user.UserID == userID
			select user;

		if (resultUser is not null && resultUser.Any())
		{
			return Task.FromResult(resultUser.First().UserIconURI);
		}
		else
		{
			return Task.FromResult(DefaultUserIconUri);
		}
	}

	public Task<int[]> GetUserLikedGameIDsAsync(int userID)
	{
		var resultUser =
			from user in _users
			where user.UserID == userID
			select user;

		if (!resultUser.Any())
		{
			return Task.FromResult(Array.Empty<int>());
		}

		return Task.FromResult(resultUser.First().LikedGameIDs.ToArray());
	}

	public Task<int[]> GetUserJoinedPostIDsAsync(int userID)
	{
		var resultUser =
			from user in _users
			where user.UserID == userID
			select user;

		if (!resultUser.Any())
		{
			return Task.FromResult(Array.Empty<int>());
		}

		return Task.FromResult(resultUser.First().JoinedPostIDs.ToArray());
	}

	public Task<(string, string)> GetUserNameIconPairAsync(int userID)
	{
		var resultUser =
			from user in _users
			where user.UserID == userID
			select user;

		if (!resultUser.Any())
		{
			return Task.FromResult((string.Empty, string.Empty));
		}
		else
		{
			return Task.FromResult(
				(resultUser.First().UserName, resultUser.First().UserIconURI));
		}
	}

	public Task<Dictionary<string, string>> GetUserGroupNameIconPairAsync(IEnumerable<int> userIDs)
	{
		var resultUsers =
			from user in _users
			where userIDs.Contains(user.UserID)
			select user;

		return Task.FromResult(resultUsers.ToDictionary(user => user.UserName, user => user.UserIconURI));
	}

	public Task<(bool, int)> CheckUserPasswordAsync(string userName, string password)
	{
        var resultUser =
            from user in _users
            where userName.Equals(user.UserName)
            select user;

        if (!resultUser.Any())
		{
			return Task.FromResult((false, -1));
		}

        if (string.IsNullOrWhiteSpace(password))
        {
            return Task.FromResult((false, -1));
        }
        else
        {
            // FIXME: Check the password
        }

		return Task.FromResult((true, resultUser.First().UserID));
    }

    public Task<bool> HasUserAsync(int userId)
	{
        using var dbContext = _dbContextFactory.CreateDbContext();
		bool result = dbContext.Users.Where(user => user.Id ==  userId).Any()
				   && _users.Any(user => user.UserID == userId);

        return Task.FromResult(result);
	}

	public Task<bool> HasUserAsync(string userName)
	{
        using var dbContext = _dbContextFactory.CreateDbContext();
        bool result = dbContext.Users.Where(user => userName.Equals(user.Name)).Any()
                   && _users.Any(user => userName.Equals(user.UserName));

        return Task.FromResult(result);
	}

	public async Task<bool> HasEmailAsync(string email)
	{
        using var dbContext = _dbContextFactory.CreateDbContext();
        bool result = await dbContext.Users.AnyAsync(user => email.Equals(user.Email))
                   && _users.Any(user => email.Equals(user.UserEmail));

        return result;
	}

	private async Task<User?> GetUserByIdAsync(int userId)
	{
        using var dbContext = _dbContextFactory.CreateDbContext();

		return await dbContext.Users.SingleOrDefaultAsync(user => user.Id == userId);
    }
}