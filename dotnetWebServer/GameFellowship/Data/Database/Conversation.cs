using System.ComponentModel.DataAnnotations;

namespace GameFellowship.Data.Database;

public class Conversation
{
	[Key]
	public int Id { get; set; }

	// TODO: consider as a foreign key ?
	public int CreatorId { get; set; }

	public DateTime SendTime { get; set; }
	public string Context { get; set; } = null!;
}