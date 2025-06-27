using AutoMapper;
using CommentModule.Domain;
using CommentModule.Services.Dtos;

namespace CommentModule;

public class MapperProfile:Profile
{
    MapperProfile()
    {
        CreateMap<Comment, CreateCommentCommand>().ReverseMap();
    }
}