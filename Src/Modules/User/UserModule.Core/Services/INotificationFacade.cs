using Common.Application;
using UserModule.Core.Commands.Notifications.Create;
using UserModule.Core.Commands.Notifications.Delete;
using UserModule.Core.Commands.Notifications.DeleteAll;
using UserModule.Core.Commands.Notifications.Seen;
using UserModule.Core.Queries.Notifications.Dtos;
using UserModule.Core.Queries.Notifications.GetByFilter;

namespace UserModule.Core.Services;

public interface INotificationFacade
{
    Task<OperationResult> Create(CreateNotificationCommand command);
    Task<OperationResult> Delete(DeleteNotificationCommand command); 
    Task<OperationResult> DeleteAll(DeleteAllNotificationCommand command);
    Task<OperationResult> Seen(SeenNotificationCommand command);
    Task<NotificationFilterResult> GetNotificationByFilter(NotificationFilterParams filterParam);
}