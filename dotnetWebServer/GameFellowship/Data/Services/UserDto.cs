using GameFellowship.Data.Database;

namespace GameFellowship.Data.Services;

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

    public UserDto(User dbUser)
    {
        Name = dbUser.Name;
        Email = dbUser.Email ?? string.Empty;
        IconUri = dbUser.IconURI;
        FollowedGameNameIconPairs = dbUser.FollowedGames.ToDictionary(game => game.Name, game => game.IconURI);
        CreatedPostIds = dbUser.CreatedPosts.Select(post => post.Id).ToArray();
        JoinedPostIds = dbUser.JoinedPosts.Select(post => post.Id).ToArray();
        FriendUserNameIconPairs = dbUser.FriendUsers.ToDictionary(user => user.Name, user => user.IconURI);
    }
}
