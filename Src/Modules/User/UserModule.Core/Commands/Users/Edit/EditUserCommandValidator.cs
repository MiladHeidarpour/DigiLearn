using FluentValidation;

namespace UserModule.Core.Commands.Users.Edit;

public class EditUserCommandValidator : AbstractValidator<EditUserCommand>
{
    public EditUserCommandValidator()
    {
        RuleFor(f => f.Name)
            .NotEmpty()
            .NotNull();

        RuleFor(f => f.Family)
            .NotEmpty()
            .NotNull();
    }
}