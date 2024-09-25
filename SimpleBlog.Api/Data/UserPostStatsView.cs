using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimpleBlog.Api.Data;

public record UserPostStatsView : IEntityTypeConfiguration<UserPostStatsView>
{
    public int UserId { get; set; }
    public required string Author { get; set; }
    public int PostsCount { get; set; }
    public void Configure(EntityTypeBuilder<UserPostStatsView> builder)
    {
        builder.ToView("user_post_stats_view")
            .HasNoKey();
    }
}