using CoreModule.Domain.TeacherAgg.Events;
using MediatR;

namespace CoreModule.Application.TeacherAgg._EventHandlers;

public class AcceptTeacherRequestEventHandler : INotificationHandler<AcceptTeacherRequestEvent>
{
    public async Task Handle(AcceptTeacherRequestEvent notification, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}