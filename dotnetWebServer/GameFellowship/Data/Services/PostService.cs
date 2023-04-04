using GameFellowship.Data.FormModels;

namespace GameFellowship.Data.Services;

public class PostService : IPostService
{
	private List<Post> posts = new() {
		new Post(
			new DateTime(2023, 2, 2, 2, 2, 2),
			"Minecraft", "种地",
			new string[] {"铁盔甲", "钻石剑", "自带干粮"},
			"种土豆谢谢茄子",
			5, 1, new int[] {1,2,3,5,6},
			new DateTime(2023,3,21), new DateTime(2023,12,29),
			"Kook", "https://www.baidu.com",
			new Conversation[] {
				new(1, new DateTime(2023, 1, 11, 11, 11, 11), "Test, Test, \r\n, Long Context Test1"),
				new(2, new DateTime(2023, 2, 22, 22, 22, 22), "Test, Test, \r\n, Long Context Test1")
			}
		),
		new Post(
			new DateTime(2023, 1, 1, 1, 1, 1),
			"Minecraft", "守村庄",
			new string[] {"钻石甲", "钻石剑", "弩"},
			"救救救救救救救救救救救",
			5, 1, new int[] {1,2,3,5}
		),
		new Post(
			new DateTime(2023, 2, 2, 2, 2, 2),
			"Destiny 2", "Raid",
			new string[] {"1810", "星火术", "速刷"},
			"来打过的谢谢",
			6, 4, new int[] {4,5},
			null, null, null, null,
			new Conversation[] {
				new(4, new DateTime(2022, 1, 11, 11, 11, 11), "Test, Test, \r\n, Long Context Test1"),
				new(5, new DateTime(2022, 2, 22, 22, 22, 22), "Test, Test, \r\n, Long Context Test1")
			}
		),
	};

	public Task<bool> CreateNewPostAsync(PostModel model, int userID)
	{
		if (userID <= 0) return Task.FromResult(false);

		Post newPost = new(model, userID, DateTime.Now);
		posts.Add(newPost);

		return Task.FromResult(true);
	}

	public Task<bool> AddNewCurrentUserAsync(int postID, int userID)
	{
		var resultPost =
			from post in posts
			where post.PostID == postID
			select post;

		if (!resultPost.Any())
			return Task.FromResult(false);

		if (!resultPost.First().CurrentUserIDs.Add(userID))
			return Task.FromResult(false);

		return Task.FromResult(true);
	}

	public Task<bool> AddNewConversationAsync(int postID, Conversation conversation)
	{
		var resultPost =
			from post in posts
			where post.PostID == postID
			select post;

		if (!resultPost.Any())
			return Task.FromResult(false);

		resultPost.First().Conversations.Add(conversation);

		return Task.FromResult(true);
	}

	public Task<bool> DeleteCurrentUserAsync(int postID, int userID)
	{
		var resultPost =
			from post in posts
			where post.PostID == postID
			select post;

		if (!resultPost.Any())
			return Task.FromResult(false);

		if (!resultPost.First().CurrentUserIDs.Remove(userID))
			return Task.FromResult(false);

		return Task.FromResult(true);
	}

	public Task<Post> GetPostAsync(int postID)
	{
		var resultPost =
			from post in posts
			where post.PostID == postID
			select post;

		if (!resultPost.Any())
			return Task.FromResult(new Post());

		return Task.FromResult(resultPost.First());
	}

	public Task<Post[]> GetPostsAsync(string gameName)
	{
		var resultPosts =
			from post in posts
			where post.GameName == gameName
			select post;

		if (!resultPosts.Any())
		{
			return Task.FromResult(Array.Empty<Post>());
		}

		return Task.FromResult(resultPosts.ToArray());
	}

	public Task<Post[]> GetPostsAsync(IEnumerable<int> postIDs)
	{
		if (!postIDs.Any())
		{
			return Task.FromResult(Array.Empty<Post>());
		}

		var resultPosts =
			from post in posts
			where postIDs.Contains(post.PostID)
			select post;

		if (!resultPosts.Any())
		{
			return Task.FromResult(Array.Empty<Post>());
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
				from post in posts
				select post.MatchType
				).Take(count);
		}
		else
		{
			resultMatchTypes = (
				from post in posts
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

	public Task<string[]> GetAudioPlatformsAsync(int count)
	{
		var resultPlatforms = (
			from post in posts
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