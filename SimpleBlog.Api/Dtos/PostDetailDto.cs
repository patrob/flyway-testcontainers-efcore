namespace SimpleBlog.Api.Dtos;

public class PostDetailDto
{
    public int PostId { get; set; } 
    public string PostTitle { get; set; } = string.Empty;
    public string PostText { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
}