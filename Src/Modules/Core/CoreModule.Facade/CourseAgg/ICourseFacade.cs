using Common.Application;
using CoreModule.Application.CourseAgg.Create;
using CoreModule.Application.CourseAgg.Edit;
using CoreModule.Application.CourseAgg.Episodes.Accept;
using CoreModule.Application.CourseAgg.Episodes.Add;
using CoreModule.Application.CourseAgg.Episodes.Delete;
using CoreModule.Application.CourseAgg.Episodes.Edit;
using CoreModule.Application.CourseAgg.Sections.Add;
using CoreModule.Application.HelperEntities.CourseStudent.Create;
using CoreModule.Application.HelperEntities.CourseStudent.Delete;
using CoreModule.Query.CourseAgg._Dtos;
using CoreModule.Query.CourseAgg.Episodes.GetById;
using CoreModule.Query.CourseAgg.GetByFilter;
using CoreModule.Query.CourseAgg.GetById;
using CoreModule.Query.CourseAgg.GetBySlug;
using MediatR;

namespace CoreModule.Facade.CourseAgg;

public interface ICourseFacade
{
    Task<OperationResult> Create(CreateCourseCommand command);
    Task<OperationResult> Edit(EditCourseCommand command);
    Task<CourseFilterResult> GetByFilter(CourseFilterParams filterParams);
    Task<CourseDto?> GetById(Guid id);
    Task<CourseDto?> GetBySlug(string slug);
    Task<EpisodeDto?> GetEpisodeById(Guid id);



    Task<OperationResult?> AddStudent(Guid courseId,Guid userId);
    Task<OperationResult?> DeleteStudent(Guid courseId, Guid userId);

    Task<OperationResult> AddSection(AddCourseSectionCommand command);
    Task<OperationResult> AddEpisode(AddCourseEpisodeCommand command);
    Task<OperationResult> EditEpisode(EditEpisodeCommand command);
    Task<OperationResult> AcceptEpisode(AcceptCourseEpisodeCommand command);
    Task<OperationResult> DeleteEpisode(DeleteCourseEpisodeCommand command);
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

    public async Task<CourseDto?> GetBySlug(string slug)
    {
        return await _mediator.Send(new GetCourseBySlugQuery(slug));
    }

    public async Task<EpisodeDto?> GetEpisodeById(Guid id)
    {
        return await _mediator.Send(new GetEpisodeByIdQuery(id));
    }

    public async Task<OperationResult?> AddStudent(Guid courseId, Guid userId)
    {
        return await _mediator.Send(new CreateCourseStudentCommand(courseId, userId));
    }

    public async Task<OperationResult?> DeleteStudent(Guid courseId, Guid userId)
    {
        return await _mediator.Send(new DeleteCourseStudentCommand(courseId, userId));
    }

    public async Task<OperationResult> AddSection(AddCourseSectionCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AddEpisode(AddCourseEpisodeCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditEpisode(EditEpisodeCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AcceptEpisode(AcceptCourseEpisodeCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> DeleteEpisode(DeleteCourseEpisodeCommand command)
    {
        return await _mediator.Send(command);
    }
}