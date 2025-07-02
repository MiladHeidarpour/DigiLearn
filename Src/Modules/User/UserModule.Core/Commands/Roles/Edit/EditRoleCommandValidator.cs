using FluentValidation;

namespace UserModule.Core.Commands.Roles.Edit;

class EditRoleCommandValidator : AbstractValidator<EditRoleCommand>
{
    public EditRoleCommandValidator()
    {
        RuleFor(r => r.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3).WithMessage("نقش باید بشتر از 3 کاراکتر باشد");
    }
}