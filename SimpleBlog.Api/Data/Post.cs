using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimpleBlog.Api.Data;

[Table("posts")]
public record Post : Entity, IEntityTypeConfiguration<Post>
{
    public required string Title { get; set; }
    public required string Text { get; set; }
    public int UserId { get; set; }
    
    public virtual required User Author { get; set; }
    
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasOne(x => x.Author)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .IsRequired();
    }
}