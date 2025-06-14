using Common.Application;
using Common.EventBus.Abstractions;
using Common.EventBus.Events;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using UserModule.Data;

namespace UserModule.Core.Commands.Users.Edit;

public class EditUserCommandHandler : IBaseCommandHandler<EditUserCommand>
{
    private readonly UserContext _context;
    private readonly IEventBus _eventBus;
    public EditUserCommandHandler(UserContext context, IEventBus eventBus)
    {
        _context = context;
        _eventBus = eventBus;
    }

    public async Task<OperationResult> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(f => f.Id == request.UserId, cancellationToken);
        if (user is null)
        {
            return OperationResult.NotFound("کاربری یافت نشد");
        }
        user.Name = request.Name;
        user.Family = request.Family;

        if (string.IsNullOrWhiteSpace(request.Email) is false)
        {
            user.Email = request.Email;
        }

        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);

        _eventBus.Publish(new UserEdited()
        {
            UserId = user.Id,
            Email = user.Email,
            Name = user.Name,
            Family = user.Family,
        }, null, Exchanges.UserTopicExchange, ExchangeType.Topic, "user.edited");

        return OperationResult.Success();
    }
}