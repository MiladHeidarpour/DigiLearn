using Common.Query;

namespace UserModule.Core.Queries.Notifications.Dtos;

public class NotificationFilterData:BaseDto
{
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public bool IsSeen { get; set; }
}