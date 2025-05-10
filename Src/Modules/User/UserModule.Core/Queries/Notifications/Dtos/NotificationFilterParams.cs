using Common.Query.Filter;

namespace UserModule.Core.Queries.Notifications.Dtos;

public class NotificationFilterParams:BaseFilterParam
{
    public Guid UserId { get; set; }
    public bool? IsSeen { get; set; }
}