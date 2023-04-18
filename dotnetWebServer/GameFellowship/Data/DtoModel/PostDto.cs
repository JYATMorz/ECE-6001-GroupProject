using GameFellowship.Data.Database;
using GameFellowship.Services;

namespace GameFellowship.Data.DtoModel;

public readonly record struct PostDto
{
    public int Id { get; init; }

    public string CreatorName { get; init; }
    public string CreatorIconUri { get; init; }
    public string GameName { get; init; }
    public string GameIconUri { get;init; }
    public DateTime LastUpdate { get; init; }

    public string MatchType { get; init; }
    public string Description { get; init; }
    public string[] Requirements { get; init; }

    public int TotalPeople { get; init; }
    public int CurrentPeople { get; init; }

    public bool PlayNow { get; init; }
    public DateTime Start { get; init; }
    public DateTime End { get; init; }

    public bool AudioChat { get; init; }
    public string AudioPlatform { get; init; }
    public string AudioLink { get; init; }

    public PostDto(Post post, User? creator = null, Game? game = null)
    {
        CreatorName = creator?.Name ?? post.Creator.Name;
        CreatorIconUri = creator?.IconURI ?? post.Creator.IconURI;
        GameName = game?.Name ?? post.Game.Name;
        GameIconUri = game?.IconURI ?? post.Game.IconURI;

        Id = post.Id;
        LastUpdate = post.LastUpdate.ToLocalTime();

        MatchType = post.MatchType;
        Description = post.Description;
        Requirements = post.Requirements.Split(PostService.DefaultConnectionSigns);
        
        TotalPeople = post.TotalPeople;
        CurrentPeople = post.CurrentPeople;

        PlayNow = post.PlayNow;
        Start = (post.StartDate ?? DateTime.Now).ToLocalTime();
        End = (post.StartDate ?? DateTime.Now).ToLocalTime();

        AudioChat = post.AudioChat;
        AudioPlatform = post.AudioPlatform ?? string.Empty;
        AudioLink = post.AudioLink ?? string.Empty;
    }
}
