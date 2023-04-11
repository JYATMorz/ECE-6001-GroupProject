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

    // 1 to many foreign key to parent Game
    public Game Game { get; set; } = null!;
	// 1 to many foreign key to creator User
	public User Creator { get; set; } = null!;
    // Many to many foreign key to joined Users
    public ICollection<User> JoinedUsers { get; set; } = new List<User>();
    // 1 to many foreign key to child Conversations
    public ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();
}