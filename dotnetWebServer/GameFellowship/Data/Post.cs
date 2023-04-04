using GameFellowship.Data.FormModels;

namespace GameFellowship.Data;

public class Post
{
    private static int _postID;

    public int PostID { get; init; } = -1;
    public int CreatorID { get; init; } = -1;
    public HashSet<int> CurrentUserIDs { get; set; } = new HashSet<int>();

    public DateTime LastUpdate { get; init; }

    public string GameName { get; set; } = string.Empty;
    public string MatchType { get; set; } = string.Empty;

    public string[] Requirements { get; set; } = Array.Empty<string>();
    public string Description { get; set; } = string.Empty;

    public int TotalPeople { get; set; } = 0;
    public int CurrentPeople { get; set; } = 0;

    public bool PlayNow { get; set; } = true;
    public DateTime StartDate { get; set; } = DateTime.MinValue;
    public DateTime EndDate { get; set; } = DateTime.MaxValue;

    public bool AudioChat { get; set; } = false;
    public string AudioPlatform { get; set; } = string.Empty;
    public string AudioLink { get; set; } = string.Empty;

    public List<Conversation> Conversations { get; set; } = new List<Conversation>();

    public Post()
    {
        PostID = ++_postID;
    }

    public Post(DateTime update, string game, string match, string[] requirements,
        string? description, int total, int creator, int[] userIDs, DateTime? start = null, DateTime? end = null,
        string? platform = null, string? link = null, Conversation[]? conversations = null)
    {
        PostID = ++_postID;

        LastUpdate = update;
        GameName = game;
        MatchType = match;
        Requirements = requirements;
        TotalPeople = total;
        CurrentPeople = userIDs.Length;
        CreatorID = creator;
        CurrentUserIDs = userIDs.ToHashSet();

        if (description is not null) Description = description;

        if (start is null || end is null)
        {
            PlayNow = true;
        }
        else
        {
            PlayNow = false;
            StartDate = (DateTime)start;
            EndDate = (DateTime)end;
        }

        if (platform is null || link is null)
        {
            AudioChat = false;
        }
        else
        {
            AudioChat = true;
            AudioPlatform = platform;
            AudioLink = link;
        }

        if (conversations is not null)
        {
            Conversations = conversations.ToList();
        }
    }

    public Post(PostModel model, int userID, DateTime update)
    {
        PostID = ++_postID;
        CreatorID = userID;
        CurrentUserIDs.Add(userID);

        LastUpdate = update;

        GameName = model.GameName;
        MatchType = model.MatchType;
        Requirements = model.Requirements;
        Description = model.Description;
        TotalPeople = model.TotalPeople;
        CurrentPeople = model.CurrentPeople;
        PlayNow = model.PlayNow;
        AudioChat = model.AudioChat;

        if (!PlayNow)
        {
            StartDate = new DateTime(
                model.StartDate.Year, model.StartDate.Month, model.StartDate.Day,
                model.StartTime.Hour, model.StartTime.Minute, model.StartTime.Second);
            EndDate = new DateTime(
                model.EndDate.Year, model.EndDate.Month, model.EndDate.Day,
                model.EndTime.Hour, model.EndTime.Minute, model.EndTime.Second);
        }

        if (AudioChat)
        {
            AudioPlatform = model.AudioPlatform;
            AudioLink = model.AudioLink;
        }
    }
}