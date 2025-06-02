using Common.Domain;
using MediatR;

namespace CoreModule.Domain.TeacherAgg.Events;

public class RejectTeacherRequestEvent: BaseDomainEvent
{
    public Guid UserId { get; set; }
    public string Description { get; set; }
}

public class AcceptTeacherRequestEvent : BaseDomainEvent
{
    public Guid UserId { get; set; }
}