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

	public DateTime? LastLogin { get; set; }

	// many to many foreign key to followed Games
	public ICollection<Game> FollowedGames { get; set; } = new List<Game>();
	// 1 to many foreign key to child created Post
	public ICollection<Post> CreatedPosts { get; set; } = new List<Post>();
	// many to many foreign key to joined Posts
	public ICollection<Post> JoinedPosts { get; set; } = new List<Post>();
	// 1 to many foreign key to created Conversations
	public ICollection<Conversation> MyConversations { get; set; } = new List<Conversation>();

	// self many to many foreign key to friend Users
	// https://learn.microsoft.com/en-us/ef/core/modeling/relationships/many-to-many#symmetrical-self-referencing-many-to-many
	public ICollection<User> FriendUsers { get; set; } = new List<User>();
}