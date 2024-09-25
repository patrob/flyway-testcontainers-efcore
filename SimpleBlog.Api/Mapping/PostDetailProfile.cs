using AutoMapper;
using SimpleBlog.Api.Data;
using SimpleBlog.Api.Dtos;

namespace SimpleBlog.Api.Mapping;

// ReSharper disable once UnusedType.Global
public class PostDetailProfile : Profile
{
    public PostDetailProfile()
    {
        CreateMap<PostDetailView, PostDetailDto>().ReverseMap();
    }
}