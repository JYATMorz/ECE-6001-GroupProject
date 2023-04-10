using System.ComponentModel.DataAnnotations;

namespace GameFellowship.Data.Database;

public class Post
{
    [Key]
    public int Id { get; set; }
    
    public DateTime LastUpdate { get; set; }

    public string MatchType { get; set; } = null!;
    public string Requirements { get; set; } = null!; // FIXME: Combine to a string after form model creation
    public string Description { get; set; } = null!;

    public int TotalPeople { get; set; }
    public int CurrentPeople { get; set; }

    public bool PlayNow { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public bool AudioChat { get; set; }
    public string? AudioPlatform { get; set; }
    public string? AudioLink { get; set; }

    // shadow foreign key property to corresponding Game
    public Game Game { get; set; } = null!;
    // shadow foreign key property to creator
    public User Creator { get; set; } = null!;

    // currently joined users
    // https://learn.microsoft.com/en-us/ef/core/modeling/relationships/many-to-many
    public ICollection<int> CurrentUserIDs { get; set; } = new List<int>();
    // conversations of the post
    // TODO: Use OwnsMany ?
    // https://learn.microsoft.com/en-us/ef/core/modeling/relationships/one-to-many
    public ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();
}