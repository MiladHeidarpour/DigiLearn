using AutoMapper;
using BlogModule.Domain;
using BlogModule.Services.Dtos.Commands;
using BlogModule.Services.Dtos.Queries;

namespace BlogModule;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Category, BlogCategoryDto>().ReverseMap();
        CreateMap<Category, CreateCategoryCommand>().ReverseMap();
        CreateMap<Category, EditCategoryCommand>().ReverseMap();

        CreateMap<BlogPostDto, Post>().ReverseMap();
        CreateMap<CreatePostCommand, Post>().ReverseMap();
        CreateMap<EditPostCommand, Post>().ReverseMap();
    }
}