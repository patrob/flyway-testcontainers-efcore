using SimpleBlog.Api.Repositories;
using SimpleBlog.Api.Data;

namespace SimpleBlog.IntegrationTests.Repositories;

public class UserRepositoryTest : BaseDataTest
{
    private readonly IUserRepository _userRepository;
    private readonly User _testUser;

    public UserRepositoryTest(DatabaseFixture databaseFixture) : base(databaseFixture)
    {
        databaseFixture.ResetDatabase().Wait();
        _userRepository = new UserRepository(Context);
        _testUser = new User
        {
            FirstName = "John",
            LastName = "Doe"
        };
    }

    [Fact]
    public void GetAllUsers_EmptyDb_ReturnsEmptyList()
    {
        var users = _userRepository.GetAllUsers();

        Assert.Empty(users);
    }

    [Fact]
    public void GetAllUsers_DbWithSingleUser_ReturnsSingletonList()
    {
        Context.Users.Add(_testUser);
        Context.SaveChanges();
            
        var users = _userRepository.GetAllUsers().ToList();

        users.Should().HaveCount(1);
        users.Single().Should().BeEquivalentTo(_testUser);
    }

    [Theory]
    [InlineData(5)]
    [InlineData(100)]
    public void GetAllUsers_DbWithMultipleUsersAllOfSameUser_ReturnsAllUsers(int userCount)
    {
        Enumerable.Range(0, userCount).ToList().ForEach(_ =>
        {
            Context.Users.Add(GetFakeModel<User>());
        });
        Context.SaveChanges();
            
        var users = _userRepository.GetAllUsers().ToList();
        users.Should().HaveCount(userCount);
    }
}