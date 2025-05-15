using FluentValidation;

namespace CoreModule.Application.TeacherAgg.Register;

public class RegisterTeacherCommandValidator : AbstractValidator<RegisterTeacherCommand>
{
    public RegisterTeacherCommandValidator()
    {
        RuleFor(f => f.UserName)
            .NotEmpty()
            .NotNull();

        RuleFor(f => f.CvFile)
            .NotEmpty()
            .NotNull();
    }
}