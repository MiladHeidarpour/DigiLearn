using Common.Application;
using CoreModule.Domain.HelperEntities.Repositories;

namespace CoreModule.Application.HelperEntities.CourseStudent.Create;

public record class CreateCourseStudentCommand(Guid CourseId, Guid UserId) : IBaseCommand
{
}
class CreateCourseStudentCommandHandler : IBaseCommandHandler<CreateCourseStudentCommand>
{
    private readonly ICourseStudentRepository _repository;
    public CreateCourseStudentCommandHandler(ICourseStudentRepository repository)
    {
        _repository = repository;
    }
    public async Task<OperationResult> Handle(CreateCourseStudentCommand request, CancellationToken cancellationToken)
    {
        var userInCourse = await _repository.ExistsAsync(f => f.UserId == request.UserId && f.CourseId == request.CourseId);
        if (userInCourse)
        {
            return OperationResult.Success();
        }

        var courseStudent = new Domain.HelperEntities.CourseStudent
        {
            CourseId = request.CourseId,
            UserId = request.UserId
        };
        _repository.Add(courseStudent);
        await _repository.Save();
        return OperationResult.Success();
    }
}