using Common.Application;
using Microsoft.EntityFrameworkCore;
using UserModule.Data;

namespace UserModule.Core.Commands.Users.Edit;

public class EditUserCommandHandler : IBaseCommandHandler<EditUserCommand>
{
    private readonly UserContext _context;

    public EditUserCommandHandler(UserContext context)
    {
        _context = context;
    }

    public async Task<OperationResult> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        var user =await _context.Users.FirstOrDefaultAsync(f => f.Id == request.UserId);
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
        return OperationResult.Success();
    }
}