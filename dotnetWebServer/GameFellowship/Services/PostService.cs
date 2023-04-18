using GameFellowship.Data.Database;
using GameFellowship.Data.DtoModel;
using GameFellowship.Data.FormModel;
using Microsoft.EntityFrameworkCore;

namespace GameFellowship.Services;

public class PostService : IPostService
{
    public static string DefaultConnectionSigns => "++";

    private readonly IDbContextFactory<GameFellowshipDb> _dbContextFactory;

    public PostService(IDbContextFactory<GameFellowshipDb> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<bool> CreatePostAsync(PostModel model, int userId)
    {
        if (userId <= 0) return false;

        using var dbContext = _dbContextFactory.CreateDbContext();

        var resultUser = await dbContext.Users
                                        .Where(user => userId == user.Id)
                                        .FirstOrDefaultAsync();
        if (resultUser is null) return false;

        var resultGame = await dbContext.Games
                                        .Where(game => game.Name == model.GameName)
                                        .FirstOrDefaultAsync();
        if (resultGame is null) return false;

        Post post = new()
        {
            LastUpdate = DateTime.Now.ToUniversalTime(),
            MatchType = model.MatchType,
            Requirements = string.Join(DefaultConnectionSigns, model.Requirements),
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
        dbContext.Posts.Add(post);
        resultGame.LastPostDate = DateTime.Now.ToUniversalTime();

        await dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeletePostAsync(int postId)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();
        var resultPost = await dbContext.Posts
                                        .Where(post => post.Id == postId)
                                        .FirstOrDefaultAsync();
        if (resultPost is null) return false;

        dbContext.Posts.Remove(resultPost);
        await dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddCurrentUserAsync(int postId, int userId)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();
        var resultPost = await dbContext.Posts
                                        .Where(post => post.Id == postId)
                                        .Include(post => post.JoinedUsers)
                                        .FirstOrDefaultAsync();
        if (resultPost is null) return false;

        var resultUser = await dbContext.Users
                                        .Where(user => user.Id == userId)
                                        .FirstOrDefaultAsync();
        if (resultUser is null || resultPost.JoinedUsers.Contains(resultUser))
        {
            return false;
        }

        resultPost.JoinedUsers.Add(resultUser);
        resultPost.CurrentPeople++;

        await dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddConversationAsync(ConversationModel model)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();
        var resultPost = await dbContext.Posts
                                        .Where(post => post.Id == model.PostId)
                                        .FirstOrDefaultAsync();
        if (resultPost is null) return false;

        var resultUser = await dbContext.Users
                                        .Where(user => user.Id == model.CreatorId)
                                        .FirstOrDefaultAsync();
        if (resultUser is null) return false;

        Conversation newConversation = new()
        {
            SendTime = model.SendTime,
            Context = model.Context,
            Post = resultPost,
            Creator = resultUser
        };
        dbContext.Conversations.Add(newConversation);
        await dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteCurrentUserAsync(int postId, int userId)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();
        var resultPost = await dbContext.Posts
                                        .Where(post => post.Id == postId)
                                        .Include(post => post.JoinedUsers)
                                        .FirstOrDefaultAsync();
        if (resultPost is null) return false;

        var resultUser = await dbContext.Users
                                        .Where(user => user.Id == userId)
                                        .FirstOrDefaultAsync();
        if (resultUser is null || !resultPost.JoinedUsers.Contains(resultUser))
        {
            return false;
        }

        if (!resultPost.JoinedUsers.Remove(resultUser))
        {
            return false;
        }
        resultPost.CurrentPeople--;

        await dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<PostDto?> GetPostAsync(int postId)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();
        var resultPost = await dbContext.Posts
                                        .Where(post => post.Id == postId)
                                        .Include(post => post.Creator).AsSplitQuery()
                                        .Include(post => post.Game).AsSplitQuery()
                                        .FirstOrDefaultAsync();
        if (resultPost is null) return null;

        return new PostDto(resultPost);
    }

    public async Task<PostDto[]> GetPostsAsync(string gameName)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();
        var resultPosts = await dbContext.Posts
                                        .Include(post => post.Game)
                                        .Where(post => post.Game.Name == gameName)
                                        .Include(post => post.Creator).AsSplitQuery()
                                        .ToArrayAsync();

        if (!resultPosts.Any()) return Array.Empty<PostDto>();

        return Array.ConvertAll(resultPosts, post => new PostDto(post));
    }

    public async Task<PostDto[]> GetPostsAsync(IEnumerable<int> postIDs)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();
        var resultPosts = await dbContext.Posts
                                        .Where(post => postIDs.Contains(post.Id))
                                        .Include(post => post.Creator).AsSplitQuery()
                                        .Include(post => post.Game).AsSplitQuery()
                                        .ToArrayAsync();

        if (!resultPosts.Any()) return Array.Empty<PostDto>();

        return Array.ConvertAll(resultPosts, post => new PostDto(post));
    }

    public async Task<string[]> GetMatchTypesAsync(int count, string? gameName = null)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();

        string[]? resultPost;

        if (string.IsNullOrWhiteSpace(gameName))
        {
            var tempPost = await dbContext.Posts
                                          .GroupBy(post => post.MatchType)
                                          .Select(group => new { Type = group.Key, Count = group.Count() })
                                          .ToListAsync();
            resultPost = tempPost.OrderBy(group => group.Count)
                                 .Select(group => group.Type)
                                 .TakeLast(count)
                                 .ToArray();
        }
        else
        {
            var tempPost = await dbContext.Posts
                                          .Include(post => post.Game)
                                          .Where(post => post.Game.Name == gameName)
                                          .GroupBy(post => post.MatchType)
                                          .Select(group => new { Type = group.Key, Count = group.Count() })
                                          .ToListAsync();
            resultPost = tempPost.OrderBy(group => group.Count)
                                 .Select(group => group.Type)
                                 .TakeLast(count)
                                 .ToArray();
        }

        return resultPost;
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

    public async Task<ConversationDto[]> GetConversations(int postId)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();
        var resultPost = await dbContext.Posts
                                        .Where(post => post.Id == postId)
                                        .Include(post => post.Conversations)
                                        .ThenInclude(con => con.Creator)
                                        .FirstOrDefaultAsync();

        if (resultPost is null) return Array.Empty<ConversationDto>();

        return Array.ConvertAll(resultPost.Conversations.ToArray(), con => new ConversationDto(con));
    }

    public async Task<string[]> GetAudioPlatformsAsync(int count)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();
        var tempPost = await dbContext.Posts
                                        .Where(post => !string.IsNullOrWhiteSpace(post.AudioPlatform))
                                        .GroupBy(post => post.AudioPlatform)
                                        .Select(group => new { Type = group.Key, Count = group.Count() })
                                        .ToListAsync();
        var resultPost = tempPost.OrderBy(group => group.Count)
                                 .Select(group => group.Type)
                                 .TakeLast(count)
                                 .ToArray();

        return resultPost!;
    }
}