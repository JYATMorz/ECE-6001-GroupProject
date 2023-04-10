using System.ComponentModel.DataAnnotations;

namespace GameFellowship.Data.Database;

public class User
{
    [Key]
    public int Id { get; set; }

    public string Password { get; set; } = null!;

    public HashSet<int> LikedGameIDs { get; set; } = null!;
	public HashSet<int> CreatedPostIDs { get; set; } = null!;
	public HashSet<int> JoinedPostIDs { get; set; } = null!;
	public HashSet<int> FriendIDs { get; set; } = null!;

	public string Name { get; set; } = null!;
    public string? Email { get; set; }
    public string IconURI { get; set; } = null!;
}