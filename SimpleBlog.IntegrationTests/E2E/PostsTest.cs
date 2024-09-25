using System.Net.Http.Json;
using SimpleBlog.Api.Data;
using SimpleBlog.Api.Dtos;

namespace SimpleBlog.IntegrationTests.E2E;

[Collection("Database")]
public class PostsTest(DatabaseFixture databaseFixture, CustomWebApplicationFactory factory) : BaseE2ETest(databaseFixture, factory)
{
    [Fact]
    public async Task GetAllPostsEndpoint_GivenEmptyDb_ShouldReturnEmpty()
    {
        var result = await Client.GetFromJsonAsync<IEnumerable<PostDto>>("api/posts");
        result.Should().BeEmpty();
    }
    
    [Fact]
    public async Task GetAllPostsEndpoint_GivenSinglePost_ShouldReturnOne()
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
        
        var result = (await Client.GetFromJsonAsync<IEnumerable<PostDto>>("api/posts"))!.ToList();
        result.Should().HaveCount(1);
        result.Single().Should().BeEquivalentTo(testPost, options => options.Excluding(p => p.Id).Excluding(p => p.UserId));
    }
}