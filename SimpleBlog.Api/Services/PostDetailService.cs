using AutoMapper;
using SimpleBlog.Api.Dtos;
using SimpleBlog.Api.Repositories;

namespace SimpleBlog.Api.Services;

public interface IPostDetailService
{
    IEnumerable<PostDetailDto> GetAllPostDetails();
}

public class PostDetailService(IPostDetailRepository postDetailRepository, IMapper mapper) : IPostDetailService
{
    public IEnumerable<PostDetailDto> GetAllPostDetails()
    {
        var postDetails = postDetailRepository.GetAllPostDetails();
        return mapper.Map<IEnumerable<PostDetailDto>>(postDetails);
    }
}