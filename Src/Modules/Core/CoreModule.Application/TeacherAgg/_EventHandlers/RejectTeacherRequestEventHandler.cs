using Common.EventBus.Abstractions;
using Common.EventBus.Events;
using CoreModule.Domain.TeacherAgg.Events;
using MediatR;
using RabbitMQ.Client;

namespace CoreModule.Application.TeacherAgg._EventHandlers;

public class RejectTeacherRequestEventHandler:INotificationHandler<RejectTeacherRequestEvent>
{
    private readonly IEventBus _eventBus;

    public RejectTeacherRequestEventHandler(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task Handle(RejectTeacherRequestEvent notification, CancellationToken cancellationToken)
    {
        _eventBus.Publish(new NewNotification
        {
            UserId = notification.UserId,
            Title = "درخواست مدرسی شما رد شد",
            Message = $"کاربر گرامی درخواست مدرسی شما به دلیل زیر رد شد : <hr/><p>{notification.Description}</p>",
            CreationDate = notification.CreationDate
        }, "", Exchanges.NotificationFanoutExvhange, ExchangeType.Fanout);
        await Task.CompletedTask;
    }
}