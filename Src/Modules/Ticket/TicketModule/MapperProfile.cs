using AutoMapper;
using TicketModule.Core.Dtos.Command;
using TicketModule.Core.Dtos.Queries;
using TicketModule.Data.Entities;

namespace TicketModule;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<CreateTicketCommand, Ticket>().ReverseMap();
        CreateMap<SendTicketMessageCommand, Ticket>().ReverseMap();
        CreateMap<TicketDto, Ticket>().ReverseMap();
        CreateMap<TicketMessageDto, TicketMessage>().ReverseMap();
    }
}