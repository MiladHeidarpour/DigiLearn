using Common.Application;
using CoreModule.Domain.TeacherAgg.Events;
using CoreModule.Domain.TeacherAgg.Repositories;
using MediatR;

namespace CoreModule.Application.TeacherAgg.RejectRequest;

public class RejectTeacherRequestCommandHandler : IBaseCommandHandler<RejectTeacherRequestCommand>
{
    private readonly ITeacherRepository _repository;
    private readonly IMediator _mediator;

    public RejectTeacherRequestCommandHandler(ITeacherRepository repository, IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }

    public async Task<OperationResult> Handle(RejectTeacherRequestCommand request, CancellationToken cancellationToken)
    {
        var teacher = await _repository.GetTracking(request.TeacherId);

        if (teacher is null)
        {
            return OperationResult.NotFound();
        }

        _repository.Delete(teacher);
        await _repository.Save();

        //send Event
        await _mediator.Publish(new RejectTeacherRequestEvent()
        {
            UserId = teacher.UserId,
            Description = request.Description
        }, cancellationToken);

        return OperationResult.Success();
    }
}