namespace SimpleBlog.Api.Dtos;

public class PostDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public UserDto Author { get; set; } = default!;
}