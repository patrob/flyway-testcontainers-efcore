using Microsoft.EntityFrameworkCore;
using SimpleBlog.Api.Data;

namespace SimpleBlog.Api.Repositories;

public interface IUserRepository
{
    IEnumerable<User> GetAllUsers();
    int AddUser(User user);
}

public class UserRepository(BlogDbContext dbContext) : IUserRepository
{
    public IEnumerable<User> GetAllUsers() => dbContext.Users
        .AsNoTracking();

    public int AddUser(User user)
    {
        dbContext.Users.Add(user);
        dbContext.SaveChanges();
        return user.Id;
    }
}