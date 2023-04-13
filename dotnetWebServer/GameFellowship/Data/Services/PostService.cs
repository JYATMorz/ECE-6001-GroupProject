using GameFellowship.Data.Database;
using GameFellowship.Data.FormModels;
using Microsoft.EntityFrameworkCore;

namespace GameFellowship.Data.Services;

public class PostService : IPostService
{
	public string ConnectionSigns => "++";

	// BUG: Not functional at all
    private List<PostTemp> _posts = new();

    private readonly IDbContextFactory<GameFellowshipDb> _dbContextFactory;

    public PostService(IDbContextFactory<GameFellowshipDb> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<(bool, int)> CreateNewPostAsync(PostModel model, int userId)
	{
		if (userId <= 0) return (false, -1);

        using var dbContext = _dbContextFactory.CreateDbContext();
		var resultUser = await dbContext.Users
										.Where(user => userId == user.Id)
										.FirstOrDefaultAsync();
		if (resultUser is null) return (false, -1);
		var resultGame = await dbContext.Games
										.Where(game => game.Name == model.GameName)
										.FirstOrDefaultAsync();
		if (resultGame is null) return (false, -1);

		Post post = new()
        {
			LastUpdate = DateTime.Now.ToUniversalTime(),
			MatchType = model.MatchType,
			Requirements = string.Join("++", model.Requirements),
			Description = model.Description,
			TotalPeople = model.TotalPeople,
			CurrentPeople = model.CurrentPeople,
			PlayNow = model.PlayNow,
			StartDate = model.PlayNow ? null : model.StartDate.ToUniversalTime(),
			EndDate = model.PlayNow ? null : model.EndDate.ToUniversalTime(),
			AudioChat = model.AudioChat,
			AudioPlatform = model.AudioChat ? null : model.AudioPlatform,
			AudioLink = model.AudioChat ? null : model.AudioLink,
			Game = resultGame,
			Creator = resultUser,
            JoinedUsers = new List<User> { resultUser }
			// FIXME: Empty Conversations ? Will it work ?
		};
		await dbContext.Posts.AddAsync(post);
		await dbContext.SaveChangesAsync();

		return (true, post.Id);
	}

	public Task<bool> AddNewCurrentUserAsync(int postID, int userID)
	{
		var resultPost =
			from post in _posts
			where post.PostID == postID
			select post;

		if (!resultPost.Any())
		{
            return Task.FromResult(false);
        }

		if (resultPost.First().CurrentUserIDs.Add(userID))
		{
			++resultPost.First().CurrentPeople;
		}

		return Task.FromResult(true);
	}

	public Task<bool> AddNewConversationAsync(int postID, ConversationTemp conversation)
	{
		var resultPost =
			from post in _posts
			where post.PostID == postID
			select post;

		if (!resultPost.Any())
			return Task.FromResult(false);

		resultPost.First().Conversations.Add(conversation);
        resultPost.First().LastUpdate = conversation.SendTime;

		return Task.FromResult(true);
	}

	public Task<bool> DeleteCurrentUserAsync(int postID, int userID)
	{
		var resultPost =
			from post in _posts
			where post.PostID == postID
			select post;

		if (!resultPost.Any())
			return Task.FromResult(false);

		if (!resultPost.First().CurrentUserIDs.Remove(userID))
			return Task.FromResult(false);

		if (--resultPost.First().CurrentPeople <= 0)
		{
			_posts.Remove(resultPost.First());
		}

        return Task.FromResult(true);
	}

	public Task<PostTemp> GetPostAsync(int postID)
	{
		var resultPost =
			from post in _posts
			where post.PostID == postID
			select post;

		if (!resultPost.Any())
			return Task.FromResult(new PostTemp());

		return Task.FromResult(resultPost.First());
	}

	public Task<PostTemp[]> GetPostsAsync(string gameName)
	{
		var resultPosts =
			from post in _posts
			where post.GameName == gameName
			select post;

		if (!resultPosts.Any())
		{
			return Task.FromResult(Array.Empty<PostTemp>());
		}

		return Task.FromResult(resultPosts.ToArray());
	}

	public Task<PostTemp[]> GetPostsAsync(IEnumerable<int> postIDs)
	{
		if (!postIDs.Any())
		{
			return Task.FromResult(Array.Empty<PostTemp>());
		}

		var resultPosts =
			from post in _posts
			where postIDs.Contains(post.PostID)
			select post;

		if (!resultPosts.Any())
		{
			return Task.FromResult(Array.Empty<PostTemp>());
		}

		return Task.FromResult(resultPosts.ToArray());
	}

	// TODO: Try add group by count
	public Task<string[]> GetMatchTypesAsync(int count, string? gameName = null)
	{
		IEnumerable<string> resultMatchTypes;

		if (string.IsNullOrWhiteSpace(gameName))
		{
			resultMatchTypes = (
				from post in _posts
				select post.MatchType
				).Take(count);
		}
		else
		{
			resultMatchTypes = (
				from post in _posts
				where post.GameName.ToLower() == gameName.ToLower()
				select post.MatchType
			).Take(count);
		}

		if (!resultMatchTypes.Any())
		{
			return Task.FromResult(Array.Empty<string>());
		}

		return Task.FromResult(resultMatchTypes.ToArray());
	}

    public async Task<int[]> GetJoinedUserIds(int postId)
	{
        using var dbContext = _dbContextFactory.CreateDbContext();
		var resultPost = await dbContext.Posts
										.Where(post => post.Id == postId)
										.Include(post => post.JoinedUsers)
										.FirstOrDefaultAsync();
		return resultPost?.JoinedUsers?.Select(user => user.Id).ToArray()
			?? Array.Empty<int>();
    }

	public async Task<Conversation[]> GetConversations(int postId)
	{
        using var dbContext = _dbContextFactory.CreateDbContext();
        var resultPost = await dbContext.Posts
                                        .Where(post => post.Id == postId)
                                        .Include(post => post.Conversations)
                                        .FirstOrDefaultAsync();

        return resultPost?.Conversations?.ToArray() ?? Array.Empty<Conversation>();
    }

    public Task<string[]> GetAudioPlatformsAsync(int count)
	{
		var resultPlatforms = (
			from post in _posts
			where post.AudioChat
			select post.AudioPlatform
			).Take(count);

		if (!resultPlatforms.Any())
		{
			return Task.FromResult(Array.Empty<string>());
		}

		return Task.FromResult(resultPlatforms.ToArray());
	}
}