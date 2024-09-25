using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimpleBlog.Api.Data;

public record PostDetailView : IEntityTypeConfiguration<PostDetailView>
{
    public int PostId { get; set; }
    public required string PostTitle { get; set; }
    public required string PostText { get; set; }
    public required string Author { get; set; }
    public void Configure(EntityTypeBuilder<PostDetailView> builder)
    {
        builder.ToView("post_detail_view")
            .HasNoKey();
    }
}