using Common.Application;
using Microsoft.EntityFrameworkCore;
using UserModule.Data;

namespace UserModule.Core.Commands.Notifications.Delete;

public class DeleteNotificationCommandHandler : IBaseCommandHandler<DeleteNotificationCommand>
{
    private readonly UserContext _context;

    public DeleteNotificationCommandHandler(UserContext context)
    {
        _context = context;
    }

    public async Task<OperationResult> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = await _context.Notifications
            .FirstOrDefaultAsync(f => f.UserId == request.UserId && f.Id == request.NotificationId, cancellationToken);

        if (notification is null)
        {
            return OperationResult.NotFound();
        }

        _context.Notifications.Remove(notification);
        await _context.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }
}