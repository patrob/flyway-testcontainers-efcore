using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimpleBlog.Api.Data;

[Table("users")]
public record User : Entity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}