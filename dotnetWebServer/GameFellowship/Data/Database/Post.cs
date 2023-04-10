using System.ComponentModel.DataAnnotations;

namespace GameFellowship.Data.Database;

public class Post
{
    [Key]
    public int Id { get; set; }

	// TODO: consider as a foreign key ?
	public int CreatorId { get; set; }

    public HashSet<int> CurrentUserIDs { get; set; } = null!;
    public DateTime LastUpdate { get; set; }

	// shadow foreign key property to corresponding Game
	public Game Game { get; set; } = null!;

    public string MatchType { get; set; } = null!;
    public string[] Requirements { get; set; } = null!;
    public string Description { get; set; } = null!;

    public int TotalPeople { get; set; }
    public int CurrentPeople { get; set; }

    public bool PlayNow { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public bool AudioChat { get; set; }
    public string? AudioPlatform { get; set; }
    public string? AudioLink { get; set; }

    public ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();
}