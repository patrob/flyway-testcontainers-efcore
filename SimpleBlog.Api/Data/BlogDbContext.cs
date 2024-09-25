using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace SimpleBlog.Api.Data;

public class BlogDbContext(DbContextOptions<BlogDbContext> options) : DbContext(options)
{
    public virtual DbSet<Post> Posts { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<PostDetailView> PostDetailViews { get; set; }
    public virtual DbSet<UserPostStatsView> UserPostStatsViews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}