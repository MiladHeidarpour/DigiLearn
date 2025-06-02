using Common.Application;
using CoreModule.Domain.TeacherAgg.Repositories;

namespace CoreModule.Application.TeacherAgg.ToggleStatus;

public record ToggleTeacherStatusCommand(Guid TeacherId) : IBaseCommand;

public class ToggleTeacherStatusCommandHandler : IBaseCommandHandler<ToggleTeacherStatusCommand>
{
    private readonly ITeacherRepository _repository;

    public ToggleTeacherStatusCommandHandler(ITeacherRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(ToggleTeacherStatusCommand request, CancellationToken cancellationToken)
    {
        var teacher = await _repository.GetTracking(request.TeacherId);
        if (teacher is null)
        {
            return OperationResult.NotFound();
        }

        teacher.ToggleStatus();
        await _repository.Save();
        return OperationResult.Success();
    }
}