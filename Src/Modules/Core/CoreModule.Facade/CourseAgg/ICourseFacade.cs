using Common.Application;
using CoreModule.Application.CourseAgg.Create;
using CoreModule.Application.CourseAgg.Edit;
using CoreModule.Query.CourseAgg._Dtos;
using CoreModule.Query.CourseAgg.GetByFilter;
using MediatR;

namespace CoreModule.Facade.CourseAgg;

public interface ICourseFacade
{
    Task<OperationResult> Create(CreateCourseCommand command);
    Task<OperationResult> Edit(EditCourseCommand command);
    Task<CourseFilterResult> GetByFilter(CourseFilterParams filterParams);
}

public class CourseFacade : ICourseFacade
{
    private readonly IMediator _mediator;

    public CourseFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> Create(CreateCourseCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditCourseCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<CourseFilterResult> GetByFilter(CourseFilterParams filterParams)
    {
        return await _mediator.Send(new GetCoursesByFilterQuery(filterParams));
    }
}