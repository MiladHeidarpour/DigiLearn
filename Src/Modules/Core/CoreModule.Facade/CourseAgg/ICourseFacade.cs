using Common.Application;
using CoreModule.Application.CourseAgg.Create;
using CoreModule.Application.CourseAgg.Edit;
using CoreModule.Application.CourseAgg.Episodes.Add;
using CoreModule.Application.CourseAgg.Sections.Add;
using CoreModule.Query.CourseAgg._Dtos;
using CoreModule.Query.CourseAgg.GetByFilter;
using CoreModule.Query.CourseAgg.GetById;
using MediatR;

namespace CoreModule.Facade.CourseAgg;

public interface ICourseFacade
{
    Task<OperationResult> Create(CreateCourseCommand command);
    Task<OperationResult> Edit(EditCourseCommand command);
    Task<CourseFilterResult> GetByFilter(CourseFilterParams filterParams);
    Task<CourseDto?> GetById(Guid id);

    Task<OperationResult> AddSection(AddCourseSectionCommand command);
    Task<OperationResult> AddEpisode(AddCourseEpisodeCommand command);
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

    public async Task<CourseDto?> GetById(Guid id)
    {
        return await _mediator.Send(new GetCourseByIdQuery(id));
    }

    public async Task<OperationResult> AddSection(AddCourseSectionCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AddEpisode(AddCourseEpisodeCommand command)
    {
        return await _mediator.Send(command); 
    }
}