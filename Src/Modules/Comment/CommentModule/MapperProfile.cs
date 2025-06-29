using AutoMapper;
using CommentModule.Domain;
using CommentModule.Services.Dtos;

namespace CommentModule;

public class MapperProfile:Profile
{
    public MapperProfile()
    {
        CreateMap<Comment, CreateCommentCommand>().ReverseMap();
    }
}