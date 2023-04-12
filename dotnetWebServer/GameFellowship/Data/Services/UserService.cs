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

	public Task<bool> AddNewLikedGameAsync(int userId, int gameID)
	{
        var resultUser =
            from user in _users
            where user.UserID == userId
            select user;

		// FIXME: How to add ?

		if (resultUser is null || !resultUser.Any())
        {
			return Task.FromResult(false);
        }

        resultUser.First().LikedGameIDs.Add(gameID);
        return Task.FromResult(true);
    }

    public Task<bool> AddNewJoinedPostAsync(int userId, int postId)
	{
        var resultUser =
            from user in _users
            where user.UserID == userId
            select user;

		// FIXME: How to add ?

        if (resultUser is null || !resultUser.Any())
        {
            return Task.FromResult(false);
        }

        resultUser.First().JoinedPostIDs.Add(postId);
        return Task.FromResult(true);
    }

    public Task<bool> AddNewCreatePostAsync(int userId, int postId)
	{
        var resultUser =
            from user in _users
            where user.UserID == userId
            select user;

		// FIXME: Is it necessary ?

        if (resultUser is null || !resultUser.Any())
        {
            return Task.FromResult(false);
        }

		resultUser.First().CreatedPostIDs.Add(postId);
        resultUser.First().JoinedPostIDs.Add(postId);
        return Task.FromResult(true);
    }

    public async Task<bool> DeleteLikedGameAsync(int userId, int gameId)
	{
        var resultUser =
            from user in _users
            where user.UserID == userId
            select user;

		using var dbContext = _dbContextFactory.CreateDbContext();
		var result = await dbContext.Users
									.Where(user => userId == user.Id)
									.Include(user => user.FollowedGames.Where(game => game.Id == gameId))
									.FirstOrDefaultAsync();

		if (result is not null)
		{
			// FIXME: Check if the filter works
			Console.WriteLine(result.FollowedGames.Count);
		}

		if (resultUser is null || !resultUser.Any())
        {
            return false;
        }

        return resultUser.First().LikedGameIDs.Remove(gameId);
    }

    public async Task<bool> DeleteJoinedPostAsync(int userId, int postId)
	{
        var resultUser =
            from user in _users
            where user.UserID == userId
            select user;

		using var dbContext = _dbContextFactory.CreateDbContext();
		var result = await dbContext.Users
									.Where(user => userId == user.Id)
									.Include(user => user.JoinedPosts.Where(post => post.Id == postId))
									.FirstOrDefaultAsync();
		if (result is not null)
		{
			// FIXME: Check if the filter works
			Console.WriteLine(result.JoinedPosts.Count);
		}

		if (resultUser is null || !resultUser.Any())
        {
            return false;
        }

        return resultUser.First().JoinedPostIDs.Remove(postId);
    }

    public async Task<bool> DeleteCreatePostAsync(int userId, int postId)
	{
        var resultUser =
            from user in _users
            where user.UserID == userId
            select user;

		using var dbContext = _dbContextFactory.CreateDbContext();
		var result = await dbContext.Users
									.Where(user => userId == user.Id)
									.Include(user => user.CreatedPosts.Where(post => post.Id == postId))
									.FirstOrDefaultAsync();
		if (result is not null)
		{
			// FIXME: Check if the filter works
			Console.WriteLine(result.CreatedPosts.Count);
		}

		if (resultUser is null || !resultUser.Any())
        {
            return false;
        }

		resultUser.First().JoinedPostIDs.Remove(postId);

        return resultUser.First().CreatedPostIDs.Remove(postId);
    }

    public async Task<string> GetUserNameAsync(int userId)
	{
		var resultUser =
			from user in _users
			where user.UserID == userId
			select user;

		using var dbContext = _dbContextFactory.CreateDbContext();
		var result = await dbContext.Users
									.Where(user => userId == user.Id)
									.FirstOrDefaultAsync();

		// return result?.Name ?? DefaultUserName;

		if (resultUser is not null && resultUser.Any())
		{
			return resultUser.First().UserName;
		}
		else
		{
			return DefaultUserName;
		}
	}

	public async Task<string> GetUserIconUriAsync(int userId)
	{
		var resultUser =
			from user in _users
			where user.UserID == userId
			select user;

		using var dbContext = _dbContextFactory.CreateDbContext();
		var result = await dbContext.Users
									.Where(user => userId == user.Id)
									.FirstOrDefaultAsync();

		// return result?.IconURI ?? DefaultUserIconUri;

		if (resultUser is not null && resultUser.Any())
		{
			return resultUser.First().UserIconURI;
		}
		else
		{
			return DefaultUserIconUri;
		}
	}

	public async Task<int[]> GetUserLikedGameIdsAsync(int userId)
	{
		var resultUser =
			from user in _users
			where user.UserID == userId
			select user;

		using var dbContext = _dbContextFactory.CreateDbContext();
		var result = await dbContext.Users
									.Where(user => userId == user.Id)
									.Include(user => user.FollowedGames)
									.FirstOrDefaultAsync();

		// TODO: No need for PostService.GetPostsAsync(int[] PostIds)
		// FIXME: return result?.FollowedGames?.ToArray() ?? Array.Empty<Post>();

		if (!resultUser.Any())
		{
			return Array.Empty<int>();
		}

		return resultUser.First().LikedGameIDs.ToArray();
	}

	public async Task<int[]> GetUserJoinedPostIdsAsync(int userId)
	{
		var resultUser =
			from user in _users
			where user.UserID == userId
			select user;

		using var dbContext = _dbContextFactory.CreateDbContext();
		var result = await dbContext.Users
									.Where(user => userId == user.Id)
									.Include(user => user.JoinedPosts)
									.FirstOrDefaultAsync();

		// TODO: No need for PostService.GetPostsAsync(int[] PostIds)
		// FIXME: return result?.JoinedPosts?.ToArray() ?? Array.Empty<Post>();

		if (!resultUser.Any())
		{
			return Array.Empty<int>();
		}

		return resultUser.First().JoinedPostIDs.ToArray();
	}

	public async Task<(string, string)> GetUserNameIconPairAsync(int userId)
	{
		var resultUser =
			from user in _users
			where user.UserID == userId
			select user;

		using var dbContext = _dbContextFactory.CreateDbContext();
		var result = await dbContext.Users
									.Where(user => userId == user.Id)
									.FirstOrDefaultAsync();

		// FIXME: return (result?.Name ?? string.Empty, result?.IconURI ?? string.Empty);

		if (!resultUser.Any())
		{
			return (string.Empty, string.Empty);
		}
		else
		{
			return (resultUser.First().UserName, resultUser.First().UserIconURI);
		}
	}

	public async Task<Dictionary<string, string>> GetUserGroupNameIconPairAsync(IEnumerable<int> userIds)
	{
		var resultUsers =
			from user in _users
			where userIds.Contains(user.UserID)
			select user;

		using var dbContext = _dbContextFactory.CreateDbContext();
		var result = await dbContext.Users
							  .Where(user => userIds.Contains(user.Id))
							  .ToDictionaryAsync(user => user.Name, user => user.IconURI);

		return resultUsers.ToDictionary(user => user.UserName, user => user.UserIconURI);
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
}