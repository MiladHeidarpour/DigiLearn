using Common.EventBus.Abstractions;
using Common.EventBus.Events;
using CoreModule.Domain.TeacherAgg.Events;
using MediatR;
using RabbitMQ.Client;

namespace CoreModule.Application.TeacherAgg._EventHandlers;

public class AcceptTeacherRequestEventHandler : INotificationHandler<AcceptTeacherRequestEvent>
{
    private readonly IEventBus _eventBus;

    public AcceptTeacherRequestEventHandler(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task Handle(AcceptTeacherRequestEvent notification, CancellationToken cancellationToken)
    {
        _eventBus.Publish(new NewNotification
        {
            UserId = notification.UserId,
            Title = "درخواست مدرسی شما تایید شد",
            Message = "تبریک! پنل مدرس شما در این لینک در دسترس است<hr/><a href='/profile/teacher/courses'>ورود</a>",
            CreationDate = notification.CreationDate
        },"",Exchanges.NotificationFanoutExvhange,ExchangeType.Fanout);
        await Task.CompletedTask;
    }
}