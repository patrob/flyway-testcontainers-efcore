using Microsoft.EntityFrameworkCore;
using SimpleBlog.Api.Data;

namespace SimpleBlog.Api.Repositories;

public interface IUserStatsRepository
{
    IEnumerable<UserPostStatsView> GetAllUserPostStats();
}

public class UserStatsRepository(BlogDbContext dbContext) : IUserStatsRepository
{
    public IEnumerable<UserPostStatsView> GetAllUserPostStats() => dbContext.UserPostStatsViews
        .AsNoTracking();
}