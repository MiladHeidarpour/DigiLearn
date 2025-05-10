using AutoMapper;
using Common.Application;
using UserModule.Data;
using UserModule.Data.Entities.Notifications;

namespace UserModule.Core.Commands.Notifications.Create;

public class CreateNotificationCommandHandler : IBaseCommandHandler<CreateNotificationCommand>
{
    private readonly UserContext _context;
    private readonly IMapper _mapper;

    public CreateNotificationCommandHandler(UserContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OperationResult> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
    {
        var model = _mapper.Map<UserNotification>(request);

        _context.Notifications.Add(model);
        await _context.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }
}