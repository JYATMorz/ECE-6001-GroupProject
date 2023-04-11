using Microsoft.EntityFrameworkCore;

namespace GameFellowship.Data.Database;

public class GameFellowshipDb : DbContext
{
	public DbSet<User> Users { get; set; }
	public DbSet<Game> Games { get; set; }
	public DbSet<Post> Posts { get; set; }
    public DbSet<Conversation> Conversations { get; set; }

    public GameFellowshipDb(DbContextOptions<GameFellowshipDb> options)
		: base(options)
	{
	}

	// https://learn.microsoft.com/zh-cn/ef/core/dbcontext-configuration/#using-a-dbcontext-factory-eg-for-blazor

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<User>()
			.HasMany(e => e.FriendUsers)
			.WithMany();
		modelBuilder.Entity<User>()
			.HasMany(e => e.CreatedPosts)
			.WithOne(e => e.Creator)
			.HasForeignKey("CreatorId")
			.IsRequired();
		modelBuilder.Entity<User>()
			.HasMany(e => e.JoinedPosts)
			.WithMany(e => e.JoinedUsers);

		base.OnModelCreating(modelBuilder);
	}
}
