using SimpleBlog.Api.Repositories;
using SimpleBlog.Api.Data;

namespace SimpleBlog.IntegrationTests.Repositories;

public class PostDetailRepositoryTest : BaseDataTest
{
    private readonly IPostDetailRepository _postDetailRepository;
    private readonly User _testUser;
    private readonly Post _testPost;
    private readonly PostDetailView _expectedPostDetailView;

    public PostDetailRepositoryTest(DatabaseFixture databaseFixture) : base(databaseFixture)
    {
        
        _postDetailRepository = new PostDetailRepository(Context);
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
        _expectedPostDetailView = new PostDetailView
        {
            PostTitle = _testPost.Title,
            PostText = _testPost.Text,
            Author = $"{_testUser.FirstName} {_testUser.LastName}"
        };
    }

    [Fact]
    public void GetAllPostDetails_EmptyDb_ReturnsEmptyList()
    {
        var postDetails = _postDetailRepository.GetAllPostDetails();

        Assert.Empty(postDetails);
    }

    [Fact]
    public void GetAllPostDetails_DbWithSinglePost_ReturnsSingletonList()
    {
        Context.Users.Add(_testUser);
        Context.Posts.Add(_testPost);
        Context.SaveChanges();
        _expectedPostDetailView.PostId = _testPost.Id;
            
        var postDetails = _postDetailRepository.GetAllPostDetails().ToList();

        postDetails.Should().HaveCount(1);
        postDetails.Single().Should().BeEquivalentTo(_expectedPostDetailView);
    }

    [Theory]
    [InlineData(5)]
    [InlineData(100)]
    public void GetAllPostDetails_DbWithMultiplePostsAllOfSameUser_ReturnsAllPosts(int postCount)
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
            
        var postDetails = _postDetailRepository.GetAllPostDetails().ToList();
        postDetails.Should().HaveCount(postCount);
        postDetails.Should().AllSatisfy(x => x.Author.Should().Be(_expectedPostDetailView.Author));
    }
}