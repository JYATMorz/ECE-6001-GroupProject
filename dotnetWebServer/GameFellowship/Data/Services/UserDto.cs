using GameFellowship.Data.Database;

namespace GameFellowship.Data.Services;

public readonly record struct UserDto
{
    public string Name { get; init; }
    public string Email { get; init; }
    public string IconUri { get; init; }
    public string[] FollowedGamesName { get; init; }
    public int[] CreatedPostIds { get; init; }
    public int[] JoinedPostIds { get; init; }
    public Dictionary<string, string> FriendUserNameNameIconPairs { get; init; }

    public UserDto(string name, string email, string iconUri, string[] followedGamesName, int[] createdPostIds,
                   int[] joinedPostIds, Dictionary<string, string> friendUserNameNameIconPairs)
    {
        Name = name;
        Email = email;
        IconUri = iconUri;
        FollowedGamesName = followedGamesName;
        CreatedPostIds = createdPostIds;
        JoinedPostIds = joinedPostIds;
        FriendUserNameNameIconPairs = friendUserNameNameIconPairs;
    }

    public UserDto(User dbUser)
    {
        Name = dbUser.Name;
        Email = dbUser.Email ?? string.Empty;
        IconUri = dbUser.IconURI;
        FollowedGamesName = dbUser.FollowedGames.Select(game => game.Name).ToArray();
        CreatedPostIds = dbUser.CreatedPosts.Select(post => post.Id).ToArray();
        JoinedPostIds = dbUser.JoinedPosts.Select(post => post.Id).ToArray();
        FriendUserNameNameIconPairs = dbUser.FriendUsers.ToDictionary(user => user.Name, user => user.IconURI);
    }
}
