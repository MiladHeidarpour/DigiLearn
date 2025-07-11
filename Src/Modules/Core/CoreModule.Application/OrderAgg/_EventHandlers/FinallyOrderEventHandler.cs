using Common.EventBus.Abstractions;
using Common.EventBus.Events;
using CoreModule.Domain.OrderAgg.Events;
using MediatR;
using RabbitMQ.Client;

namespace CoreModule.Application.OrderAgg._EventHandlers;

public class FinallyOrderEventHandler : INotificationHandler<FinallyOrderEvent>
{
    private readonly IEventBus _eventBus;

    public FinallyOrderEventHandler(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task Handle(FinallyOrderEvent notification, CancellationToken cancellationToken)
    {
        _eventBus.Publish(new NewNotification()
        {
            Message = "فاکتور شما با موفقیت پرداخت شد",
            Title = "پرداخت موفق",
            UserId = notification.UserId
        }, null, Exchanges.NotificationFanoutExvhange, ExchangeType.Fanout);
        await Task.CompletedTask;
    }
}