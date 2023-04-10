using System.ComponentModel.DataAnnotations;

namespace GameFellowship.Data.Database;

public class Conversation
{
	[Key]
	public int Id { get; set; }

	public DateTime SendTime { get; set; }
	public string Context { get; set; } = null!;

    // foreign key, 1 to 1
    public User Creator { get; set; } = null!;
}