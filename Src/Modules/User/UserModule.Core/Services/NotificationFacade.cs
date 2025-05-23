﻿using Common.Application;
using MediatR;
using UserModule.Core.Commands.Notifications.Create;
using UserModule.Core.Commands.Notifications.Delete;
using UserModule.Core.Commands.Notifications.DeleteAll;
using UserModule.Core.Commands.Notifications.Seen;
using UserModule.Core.Queries.Notifications.Dtos;
using UserModule.Core.Queries.Notifications.GetByFilter;

namespace UserModule.Core.Services;

class NotificationFacade : INotificationFacade
{
    private readonly IMediator _mediator;

    public NotificationFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> Create(CreateNotificationCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Delete(DeleteNotificationCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> DeleteAll(DeleteAllNotificationCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Seen(SeenNotificationCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<NotificationFilterResult> GetNotificationByFilter(NotificationFilterParams filterParam)
    {
        return await _mediator.Send(new GetNotificationByFilterQuery(filterParam));
    }
}