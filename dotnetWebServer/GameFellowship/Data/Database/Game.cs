using System.ComponentModel.DataAnnotations;

namespace GameFellowship.Data.Database;

public class Game
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    public int Followers { get; set; }
    public string IconURI { get; set; } = null!;
	public DateTime LastPostDate { get; set; }

    // 1 to many foreign key to child Posts
    public ICollection<Post> Posts { get; set; } = new List<Post>();
    // many to many foreign key to following Users
    public ICollection<User> FollowingUsers { get; set; } = new List<User>();
}