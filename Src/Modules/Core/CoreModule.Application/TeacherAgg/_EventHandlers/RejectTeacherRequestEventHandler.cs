using CoreModule.Domain.TeacherAgg.Events;
using MediatR;

namespace CoreModule.Application.TeacherAgg._EventHandlers;

public class RejectTeacherRequestEventHandler:INotificationHandler<RejectTeacherRequestEvent>
{
    public async Task Handle(RejectTeacherRequestEvent notification, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}