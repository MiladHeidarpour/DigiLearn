﻿using Common.Application;
using TicketModule.Core.Dtos.Commands;
using TicketModule.Core.Dtos.Queries;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TicketModule.Core.Services;

public interface ITicketService
{
    //Command
    Task<OperationResult<Guid>> CreateTicket(CreateTicketCommand command);
    Task<OperationResult> SendMessageInTicket(SendTicketMessageCommand command);
    Task<OperationResult> CloseTicket(Guid ticketId);

    //Query
    Task<TicketDto?> GetTicketById(Guid ticketId);
    Task<TicketFilterResult> GetTicketsByFilter(TicketFilterParams filterParams);
}