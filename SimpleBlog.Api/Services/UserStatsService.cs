using AutoMapper;
using SimpleBlog.Api.Data;
using SimpleBlog.Api.Dtos;
using SimpleBlog.Api.Repositories;

namespace SimpleBlog.Api.Services;

public interface IUserStatsService
{
    IEnumerable<UserPostStatsDto> GetAllUserPostStats();
}

public class UserStatsService(IUserStatsRepository userStatsRepository, IMapper mapper) : IUserStatsService
{
    public IEnumerable<UserPostStatsDto> GetAllUserPostStats()
    {
        var userPostStats = userStatsRepository.GetAllUserPostStats();
        return mapper.Map<IEnumerable<UserPostStatsDto>>(userPostStats);
    }
}