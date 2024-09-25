using Microsoft.AspNetCore.Mvc;
using SimpleBlog.Api.Dtos;
using SimpleBlog.Api.Services;

namespace SimpleBlog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpGet]
    public IEnumerable<UserDto> GetAll()
    {
        return userService.GetAllUsers();
    }

    [HttpPost]
    public int Post([FromBody] UserDto userDto)
    {
        return userService.AddUser(userDto);
    }
}