namespace SimpleBlog.Api.Dtos;

public class UserPostStatsDto
{
    public int UserId { get; set; } 
    public string Author { get; set; } = string.Empty;
    public int PostsCount { get; set; }
}