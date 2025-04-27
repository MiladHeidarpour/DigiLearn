using Common.Application;

namespace UserModule.Core.Commands.ChangePassword;

public class ChangeUserPasswordCommand : IBaseCommand
{
    public Guid UserId { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}