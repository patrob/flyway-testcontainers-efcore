using System.Net.Http.Json;
using SimpleBlog.Api.Data;
using SimpleBlog.Api.Dtos;

namespace SimpleBlog.IntegrationTests.E2E;

[Collection("Database")]
public class PostDetailsTest(DatabaseFixture databaseFixture, CustomWebApplicationFactory factory) : BaseE2ETest(databaseFixture, factory)
{
    [Fact]
    public async Task GetAllPostDetailsEndpoint_GivenEmptyDb_ShouldReturnEmpty()
    {
        var result = await Client.GetFromJsonAsync<IEnumerable<PostDetailDto>>("api/postdetails");
        result.Should().BeEmpty();
    }
    
    [Fact]
    public async Task GetAllPostDetailsEndpoint_GivenSinglePostDetail_ShouldReturnOne()
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
        var expectedPostDetails = new PostDetailDto
        {
            PostId = testPost.Id,
            Author = $"{testUser.FirstName} {testUser.LastName}",
            PostText = testPost.Text,
            PostTitle = testPost.Title
        };
        
        var result = (await Client.GetFromJsonAsync<IEnumerable<PostDetailDto>>("api/postdetails"))!.ToList();
        result.Should().HaveCount(1);
        result.Single().Should().BeEquivalentTo(expectedPostDetails);
    }
}