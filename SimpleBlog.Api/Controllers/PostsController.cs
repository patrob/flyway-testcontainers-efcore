using Microsoft.AspNetCore.Mvc;
using SimpleBlog.Api.Dtos;
using SimpleBlog.Api.Services;

namespace SimpleBlog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController(IPostService postService) : ControllerBase
{
    [HttpGet]
    public IEnumerable<PostDto> GetAll()
    {
        return postService.GetAllPosts();
    }
}