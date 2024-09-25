using AutoMapper;
using SimpleBlog.Api.Data;
using SimpleBlog.Api.Dtos;

namespace SimpleBlog.Api.Mapping;

// ReSharper disable once UnusedType.Global
public class UserPostStatsProfile : Profile
{
    public UserPostStatsProfile()
    {
        CreateMap<UserPostStatsView, UserPostStatsDto>().ReverseMap();
    }
}