using AutoMapper;
using SimpleBlog.Api.Dtos;
using SimpleBlog.Api.Repositories;

namespace SimpleBlog.Api.Services;

public interface IPostService
{
    IEnumerable<PostDto> GetAllPosts();
}

public class PostService(IPostRepository postRepository, IMapper mapper) : IPostService
{
    public IEnumerable<PostDto> GetAllPosts()
    {
        var posts = postRepository.GetAllPosts();
        return mapper.Map<IEnumerable<PostDto>>(posts);
    }
}