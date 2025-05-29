using Common.Application;
using Common.Application.FileUtil.Interfaces;
using CoreModule.Application._Utilities;
using CoreModule.Domain.CourseAgg.Models;
using CoreModule.Domain.CourseAgg.Repositories;

namespace CoreModule.Application.CourseAgg.Episodes.Delete;

public record DeleteCourseEpisodeCommand(Guid CourseId,Guid EpisodeId) : IBaseCommand;

public class DeleteCourseEpisodeCommandHandler:IBaseCommandHandler<DeleteCourseEpisodeCommand>
{
    private readonly ICourseRepository _repository;
    private readonly ILocalFileService _localFileService;

    public DeleteCourseEpisodeCommandHandler(ICourseRepository repository, ILocalFileService localFileService)
    {
        _repository = repository;
        _localFileService = localFileService;
    }

    public async Task<OperationResult> Handle(DeleteCourseEpisodeCommand request, CancellationToken cancellationToken)
    {
        var course = await _repository.GetTracking(request.CourseId);
        if (course == null)
        {
            return OperationResult.NotFound();
        }

        var episode = course.DeleteEpisode(request.EpisodeId);
        await _repository.Save();
        _localFileService.DeleteDirectory(CoreModuleDirectories.CourseEpisode(course.Id, episode.Token));
        return OperationResult.Success();
    }
}