using SimpleBlog.Api.Repositories;
using SimpleBlog.Api.Data;

namespace SimpleBlog.IntegrationTests.Repositories;

public class PostRepositoryTest : BaseDataTest
{
    private readonly IPostRepository _postRepository;
    private readonly User _testUser;
    private readonly Post _testPost;

    public PostRepositoryTest(DatabaseFixture databaseFixture) : base(databaseFixture)
    {
        
        _postRepository = new PostRepository(Context);
        _testUser = new User
        {
            FirstName = "John",
            LastName = "Doe"
        };
        _testPost = new Post
        {
            Author = _testUser,
            Text = "This is a test",
            Title = "This is a test"
        };
    }

    [Fact]
    public void GetAllPosts_EmptyDb_ReturnsEmptyList()
    {
        var posts = _postRepository.GetAllPosts();

        Assert.Empty(posts);
    }

    [Fact]
    public void GetAllPosts_DbWithSinglePost_ReturnsSingletonList()
    {
        Context.Users.Add(_testUser);
        Context.Posts.Add(_testPost);
        Context.SaveChanges();
            
        var posts = _postRepository.GetAllPosts().ToList();

        posts.Should().HaveCount(1);
        posts.Single().Should().BeEquivalentTo(_testPost);
    }

    [Theory]
    [InlineData(5)]
    [InlineData(100)]
    public void GetAllPosts_DbWithMultiplePostsAllOfSameUser_ReturnsAllPosts(int postCount)
    {
        Context.Users.Add(_testUser);
        Enumerable.Range(0, postCount).ToList().ForEach(_ =>
        {
            Context.Posts.Add(GetFakeModel<Post>(x =>
            {
                x.Author = _testUser;
                return x;
            }));
        });
        Context.SaveChanges();
            
        var posts = _postRepository.GetAllPosts().ToList();
        posts.Should().HaveCount(postCount);
        posts.Should().AllSatisfy(x => x.UserId.Should().Be(_testUser.Id));
    }
}