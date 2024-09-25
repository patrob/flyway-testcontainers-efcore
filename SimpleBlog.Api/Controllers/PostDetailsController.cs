using Microsoft.AspNetCore.Mvc;
using SimpleBlog.Api.Dtos;
using SimpleBlog.Api.Services;

namespace SimpleBlog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostDetailsController(IPostDetailService postDetailService) : ControllerBase
{
    [HttpGet]
    public IEnumerable<PostDetailDto> GetAll()
    {
        return postDetailService.GetAllPostDetails();
    }
}