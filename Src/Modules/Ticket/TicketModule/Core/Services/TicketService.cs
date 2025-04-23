using AutoMapper;
using Common.Application;
using Common.Application.SecurityUtil;
using Microsoft.EntityFrameworkCore;
using TicketModule.Core.Dtos.Command;
using TicketModule.Core.Dtos.Queries;
using TicketModule.Data;
using TicketModule.Data.Entities;

namespace TicketModule.Core.Services;

class TicketService : ITicketService
{
    private readonly TicketContext _context;
    private readonly IMapper _mapper;

    public TicketService(TicketContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OperationResult<Guid>> CreateTicket(CreateTicketCommand command)
    {
        var ticket = _mapper.Map<Ticket>(command);

        _context.Ticket.Add(ticket);
        await _context.SaveChangesAsync();
        return OperationResult<Guid>.Success(ticket.Id);
    }

    public async Task<OperationResult> SendMessageInTicket(SendTicketMessageCommand command)
    {
        var ticket = await _context.Ticket.FirstOrDefaultAsync(f => f.Id == command.TicketId);
        if (ticket == null)
            return OperationResult.NotFound("تیکتی یافت نشد");

        var message = new TicketMessage()
        {
            Text = command.Text.SanitizeText(),
            TicketId = command.TicketId,
            UserId = command.UserId,
            UserFullName = command.OwnerFullName
        };

        if (ticket.UserId == command.UserId)
        {
            ticket.TicketStatus = TicketStatus.Pending;
        }
        else
        {
            ticket.TicketStatus = TicketStatus.Answered;
        }

        _context.TicketMessages.Add(message);
        _context.Ticket.Update(ticket);
        await _context.SaveChangesAsync();
        return OperationResult.Success();
    }

    public async Task<OperationResult> CloseTicket(Guid ticketId)
    {
        var ticket = await _context.Ticket.FirstOrDefaultAsync(f => f.Id == ticketId);
        if (ticket == null)
            return OperationResult.NotFound("تیکتی یافت نشد");

        ticket.TicketStatus = TicketStatus.Closed;

        _context.Ticket.Update(ticket);
        await _context.SaveChangesAsync();
        return OperationResult.Success();
    }

    public async Task<TicketDto> GetTicketById(Guid ticketId)
    {
        var ticket = await _context.Ticket
            .Include(f => f.TicketMessages)
            .FirstOrDefaultAsync(f => f.Id == ticketId);

        return _mapper.Map<TicketDto>(ticket.TicketMessages);
    }
}