using Common.Application;
using Common.Application.SecurityUtil;
using Microsoft.EntityFrameworkCore;
using UserModule.Data;

namespace UserModule.Core.Commands.Users.ChangePassword;

public class ChangeUserPasswordCommandHandler : IBaseCommandHandler<ChangeUserPasswordCommand>
{
    private readonly UserContext _context;

    public ChangeUserPasswordCommandHandler(UserContext context)
    {
        _context = context;
    }

    public async Task<OperationResult> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(f => f.Id == request.UserId);

        if (user is null)
            return OperationResult.NotFound("کاربری یافت نشد");

        if (Sha256Hasher.IsCompare(user.Password, request.CurrentPassword))
        {
            user.Password = Sha256Hasher.Hash(request.NewPassword);
            _context.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
            return OperationResult.Success("رمز با موفقیت تغییر کرد");
        }

        return OperationResult.Error("کلمه عبور نامعتبر است");
    }
}