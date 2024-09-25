using Microsoft.EntityFrameworkCore;
using SimpleBlog.Api.Data;

namespace SimpleBlog.Api.Repositories;

public interface IPostDetailRepository
{
    IEnumerable<PostDetailView> GetAllPostDetails();
}

public class PostDetailRepository(BlogDbContext dbContext) : IPostDetailRepository
{
    public IEnumerable<PostDetailView> GetAllPostDetails() => dbContext.PostDetailViews
        .AsNoTracking();
}