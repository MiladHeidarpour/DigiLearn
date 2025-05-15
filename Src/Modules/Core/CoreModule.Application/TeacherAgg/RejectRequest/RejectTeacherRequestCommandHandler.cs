using Common.Application;
using CoreModules.Domain.TeacherAgg.Repositories;

namespace CoreModule.Application.TeacherAgg.RejectRequest;

public class RejectTeacherRequestCommandHandler : IBaseCommandHandler<RejectTeacherRequestCommand>
{
    private readonly ITeacherRepository _repository;

    public RejectTeacherRequestCommandHandler(ITeacherRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(RejectTeacherRequestCommand request, CancellationToken cancellationToken)
    {
        var teacher = await _repository.GetTracking(request.TeacherId);

        if (teacher is null)
        {
            return OperationResult.NotFound();
        }

        _repository.Delete(teacher);
        //send Event
        await _repository.Save();
        return OperationResult.Success();
    }
}