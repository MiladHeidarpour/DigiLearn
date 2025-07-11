﻿using Common.Application;
using CoreModule.Domain.HelperEntities.Repositories;

namespace CoreModule.Application.HelperEntities.CourseStudent.Delete;

public record class DeleteCourseStudentCommand(Guid CourseId, Guid UserId) : IBaseCommand
{
}

class DeleteCourseStudentCommandHandler : IBaseCommandHandler<DeleteCourseStudentCommand>
{
    private readonly ICourseStudentRepository _repository;
    public DeleteCourseStudentCommandHandler(ICourseStudentRepository repository)
    {
        _repository = repository;
    }
    public async Task<OperationResult> Handle(DeleteCourseStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _repository.GetCourseStudent(request.CourseId, request.UserId);
        if (student == null)
        {
            return OperationResult.NotFound();
        }


        _repository.Delete(student);
        await _repository.Save();
        return OperationResult.Success();
    }
}