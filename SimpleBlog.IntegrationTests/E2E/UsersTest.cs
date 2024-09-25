using System.Net.Http.Json;
using SimpleBlog.Api.Data;
using SimpleBlog.Api.Dtos;

namespace SimpleBlog.IntegrationTests.E2E;

[Collection("Database")]
public class UsersTest(DatabaseFixture databaseFixture, CustomWebApplicationFactory factory) : BaseE2ETest(databaseFixture, factory)
{
    [Fact]
    public async Task GetAllUsersEndpoint_GivenEmptyDb_ShouldReturnEmpty()
    {
        var result = await Client.GetFromJsonAsync<IEnumerable<UserDto>>("api/users");
        result.Should().BeEmpty();
    }
    
    [Fact]
    public async Task GetAllUsersEndpoint_GivenSingleUser_ShouldReturnOne()
    {
        var testUser = GetFakeModel<User>();
        Context.Users.Add(testUser);
        await Context.SaveChangesAsync();
        
        var result = (await Client.GetFromJsonAsync<IEnumerable<UserDto>>("api/users"))!.ToList();
        result.Should().HaveCount(1);
        result.Single().Should().BeEquivalentTo(testUser);
    }

    [Fact]
    public async Task CreateUserEndpoint_ShouldCreateUser()
    {
        var testUser = new UserDto
        {
            FirstName = GetFakeModel<string>(),
            LastName = GetFakeModel<string>()
        };
        
        var response = await Client.PostAsJsonAsync("api/users", testUser);
        response.EnsureSuccessStatusCode();
        
        var actualUsers = Context.Users.ToList();
        actualUsers.Should().HaveCount(1);
        actualUsers.Single().Should().BeEquivalentTo(testUser, options => options.Excluding(x => x.Id));
    }
}