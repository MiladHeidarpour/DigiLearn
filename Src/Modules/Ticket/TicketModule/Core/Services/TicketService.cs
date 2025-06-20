﻿using AutoMapper;
using Common.Application;
using Common.Application.SecurityUtil;
using Microsoft.EntityFrameworkCore;
using TicketModule.Core.Dtos.Commands;
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

        if (string.IsNullOrWhiteSpace(command.Text))
            return OperationResult.Error("متن پیام را وراد کنید");

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

    public async Task<TicketDto?> GetTicketById(Guid ticketId)
    {
        var ticket = await _context.Ticket
            .Include(f => f.TicketMessages)
            .FirstOrDefaultAsync(f => f.Id == ticketId);

        return _mapper.Map<TicketDto>(ticket);
    }

    public async Task<TicketFilterResult> GetTicketsByFilter(TicketFilterParams filterParams)
    {
        var result = _context.Ticket.AsQueryable();

        if (filterParams.UserId is not null)
        {
            result = result.Where(f => f.UserId == filterParams.UserId);
        }

        if (string.IsNullOrWhiteSpace(filterParams.Title) ==false)
        {
            result = result.Where(f => f.Title.Contains(filterParams.Title));
        }

        if (filterParams.TicketStatus is not null)
        {
            result = result.Where(f => f.TicketStatus == filterParams.TicketStatus);
        }

        var skip = (filterParams.PageId - 1) * filterParams.Take;
        var data = new TicketFilterResult()
        {
            Data = await result.Skip(skip).Take(filterParams.Take)
                .Select(n => new TicketFilterData
                {
                    Id = n.Id,
                    UserId = n.UserId,
                    Title = n.Title,
                    OwnerFullName = n.OwnerFullName,
                    Status = n.TicketStatus,
                    CreationDate = n.CreationDate
                }).ToListAsync()
        };
        data.GeneratePaging(result, filterParams.Take, filterParams.PageId);
        return data;
    }
}