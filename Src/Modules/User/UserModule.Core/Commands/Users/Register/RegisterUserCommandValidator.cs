using FluentValidation;

namespace UserModule.Core.Commands.Users.Register;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(f => f.Password)
            .NotEmpty()
            .NotNull()
            .MinimumLength(6);
    }
}