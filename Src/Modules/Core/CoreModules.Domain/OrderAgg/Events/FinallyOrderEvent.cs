using Common.Domain;

namespace CoreModule.Domain.OrderAgg.Events;

public class FinallyOrderEvent:BaseDomainEvent
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
}