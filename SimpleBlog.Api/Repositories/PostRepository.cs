using Microsoft.EntityFrameworkCore;
using SimpleBlog.Api.Data;

namespace SimpleBlog.Api.Repositories;

public interface IPostRepository
{
    IEnumerable<Post> GetAllPosts();
}

public class PostRepository(BlogDbContext dbContext) : IPostRepository
{
    public IEnumerable<Post> GetAllPosts() => dbContext.Posts
        .Include(p => p.Author)
        .AsNoTracking();
}