using System.Net.Http.Json;
using Microsoft.OpenApi.Validations.Rules;
using SimpleBlog.Api.Data;
using SimpleBlog.Api.Dtos;

namespace SimpleBlog.IntegrationTests.E2E;

[Collection("Database")]
public class UserStatsTest(DatabaseFixture databaseFixture, CustomWebApplicationFactory factory) : BaseE2ETest(databaseFixture, factory)
{
    [Fact]
    public async Task GetUserPostStatsEndpoint_GivenEmptyDb_ShouldReturnEmpty()
    {
        var result = await Client.GetFromJsonAsync<IEnumerable<UserDto>>("api/userstats/poststats");
        result.Should().BeEmpty();
    }
    
    [Fact]
    public async Task GetUserPostStatsEndpoint_GivenSingleUserPostStat_ShouldReturnOne()
    {
        var testUser = GetFakeModel<User>();
        var testPost = GetFakeModel<Post>(x =>
        {
            x.Author = testUser;
            return x;
        });
        Context.Users.Add(testUser);
        Context.Posts.Add(testPost);
        await Context.SaveChangesAsync();
        
        var result = (await Client.GetFromJsonAsync<IEnumerable<UserPostStatsDto>>("api/userstats/poststats"))!.ToList();
        result.Should().HaveCount(1);
        result.Single().Author.Should().Contain(testUser.FirstName).And.Contain(testUser.LastName);
        result.Single().PostsCount.Should().Be(1);
    }
}