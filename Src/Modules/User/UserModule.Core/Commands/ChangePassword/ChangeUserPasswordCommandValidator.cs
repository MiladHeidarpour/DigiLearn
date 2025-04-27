using FluentValidation;

namespace UserModule.Core.Commands.ChangePassword;

public class ChangeUserPasswordCommandValidator : AbstractValidator<ChangeUserPasswordCommand>
{
    public ChangeUserPasswordCommandValidator()
    {
        RuleFor(f => f.NewPassword)
            .NotEmpty()
            .NotNull()
            .MaximumLength(6);

        RuleFor(f => f.CurrentPassword)
            .NotEmpty()
            .NotNull()
            .MaximumLength(6);
    }
}