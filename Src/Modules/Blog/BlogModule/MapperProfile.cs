using AutoMapper;
using BlogModule.Domain;
using BlogModule.Services.Dtos.Commands;
using BlogModule.Services.Dtos.Queries;

namespace BlogModule;

public class MapperProfile:Profile
{
    public MapperProfile()
    {
        CreateMap<Category, CreateCategoryCommand>().ReverseMap();
        CreateMap<Category, EditCategoryCommand>().ReverseMap();
        CreateMap<Category, BlogCategoryDto>().ReverseMap();
    }
}