using AutoMapper;
using SimpleBlog.Api.Data;
using SimpleBlog.Api.Dtos;
using SimpleBlog.Api.Repositories;

namespace SimpleBlog.Api.Services;

public interface IUserService
{
    IEnumerable<UserDto> GetAllUsers();
    int AddUser(UserDto userDto);
}

public class UserService(IUserRepository userRepository, IMapper mapper) : IUserService
{
    public IEnumerable<UserDto> GetAllUsers()
    {
        var users = userRepository.GetAllUsers();
        return mapper.Map<IEnumerable<UserDto>>(users);
    }
    
    public int AddUser(UserDto userDto)
    {
        var user = mapper.Map<User>(userDto);
        return userRepository.AddUser(user);
    }
}