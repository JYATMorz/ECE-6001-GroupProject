using System.ComponentModel.DataAnnotations;

namespace GameFellowship.Data.Database;

public class Conversation
{
	[Key]
	public int Id { get; set; }

    public DateTime SendTime { get; set; }
	public string Context { get; set; } = null!;

    // 1 to many foreign key to parent Post
    public Post Post { get; set; } = null!;
	// 1 to many foreign key to creator User
	public User Creator { get; set; } = null!;
}