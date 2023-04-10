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

    public ICollection<Post> Posts { get; set; } = new List<Post>();
}