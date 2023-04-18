using GameFellowship.Data.Database;

namespace GameFellowship.Data.DtoModel;

public readonly record struct GameDto
{
    public string Name { get; init; }
    public string IconUri { get; init; }
    public DateTime LastPost { get; init; }
    public int Followers { get; init; }
    public int Posts { get; init; }

    public GameDto(Game game, int? followers = null, int? posts = null)
    {
        Name = game.Name;
        IconUri = game.IconURI;
        LastPost = game.LastPostDate;

        Followers = followers ?? game.FollowingUsers.Count;
        Posts = posts ?? game.Posts.Count;
    }
}
