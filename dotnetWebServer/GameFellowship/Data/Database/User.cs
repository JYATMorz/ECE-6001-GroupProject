using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GameFellowship.Data.Database;

public class User
{
    [Key]
    public int Id { get; set; }

    public string Password { get; set; } = null!;

	public string Name { get; set; } = null!;
    public string? Email { get; set; }
    public string IconURI { get; set; } = null!;

    // https://learn.microsoft.com/zh-cn/ef/core/modeling/owned-entities
    [Required]
    public UserRelations UserRelations { get; set; } = null!;
}

[Owned]
public class UserRelations
{
    // https://learn.microsoft.com/en-us/ef/core/modeling/relationships/many-to-many
    public ICollection<Game> LikedGames { get; set; } = new List<Game>();
    public ICollection<Post> CreatedPosts { get; set; } = new List<Post>();
    public ICollection<Post> JoinedPosts { get; set; } = new List<Post>();
    public ICollection<User> FriendUsers { get; set; } = new List<User>();
}