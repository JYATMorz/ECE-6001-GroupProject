using GameFellowship.Data.Database;

namespace GameFellowship.Data.DtoModel;

public readonly record struct UserDto
{
    public string Name { get; init; }
    public string Email { get; init; }
    public string IconUri { get; init; }
    public Dictionary<string, string> FollowedGameNameIconPairs { get; init; }
    public int[] CreatedPostIds { get; init; }
    public int[] JoinedPostIds { get; init; }
    public Dictionary<string, string> FriendUserNameIconPairs { get; init; }

    public UserDto(string name, string email, string iconUri, Dictionary<string, string> followedGames, int[] createdPostIds,
                   int[] joinedPostIds, Dictionary<string, string> friendUsers)
    {
        Name = name;
        Email = email;
        IconUri = iconUri;
        FollowedGameNameIconPairs = followedGames;
        CreatedPostIds = createdPostIds;
        JoinedPostIds = joinedPostIds;
        FriendUserNameIconPairs = friendUsers;
    }

    public UserDto(User dbUser,
                   Dictionary<string, string>? followedGames = null,
                   int[]? createdPostIds = null,
                   int[]? joinedPostIds = null,
                   Dictionary<string, string>? friendUsers = null)
    {
        Name = dbUser.Name;
        Email = dbUser.Email ?? string.Empty;
        IconUri = dbUser.IconURI;
        FollowedGameNameIconPairs = followedGames ?? dbUser.FollowedGames.ToDictionary(game => game.Name, game => game.IconURI);
        CreatedPostIds = createdPostIds ?? dbUser.CreatedPosts.Select(post => post.Id).ToArray();
        JoinedPostIds = joinedPostIds ?? dbUser.JoinedPosts.Select(post => post.Id).ToArray();
        FriendUserNameIconPairs = friendUsers ?? dbUser.FriendUsers.ToDictionary(user => user.Name, user => user.IconURI);
    }
}
