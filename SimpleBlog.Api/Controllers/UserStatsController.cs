using Microsoft.AspNetCore.Mvc;
using SimpleBlog.Api.Dtos;
using SimpleBlog.Api.Services;

namespace SimpleBlog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserStatsController(IUserStatsService userStatsService) : ControllerBase
{
    [HttpGet("poststats")]
    public IEnumerable<UserPostStatsDto> GetAllUserPostStats()
    {
        return userStatsService.GetAllUserPostStats();
    }
}