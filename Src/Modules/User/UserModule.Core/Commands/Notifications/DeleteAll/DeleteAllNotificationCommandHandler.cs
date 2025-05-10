using Common.Application;
using Microsoft.EntityFrameworkCore;
using UserModule.Data;

namespace UserModule.Core.Commands.Notifications.DeleteAll;

public class DeleteAllNotificationCommandHandler : IBaseCommandHandler<DeleteAllNotificationCommand>
{
    private readonly UserContext _context;

    public DeleteAllNotificationCommandHandler(UserContext context)
    {
        _context = context;
    }

    public async Task<OperationResult> Handle(DeleteAllNotificationCommand request, CancellationToken cancellationToken)
    {
        var notifications = await _context.Notifications.Where(f => f.UserId == request.UserId).ToListAsync(cancellationToken);

        if (notifications.Any())
        {
            _context.Notifications.RemoveRange(notifications);
            await _context.SaveChangesAsync(cancellationToken);

        }
        return OperationResult.Success();
    }
}