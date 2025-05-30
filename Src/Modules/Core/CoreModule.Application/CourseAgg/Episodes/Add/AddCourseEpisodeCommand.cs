using AngleSharp.Io;
using Common.Application;
using Common.Application.FileUtil;
using Common.Application.FileUtil.Interfaces;
using Common.Domain.Utils;
using CoreModule.Application._Utilities;
using CoreModule.Domain.CourseAgg.Models;
using CoreModule.Domain.CourseAgg.Repositories;
using Microsoft.AspNetCore.Http;

namespace CoreModule.Application.CourseAgg.Episodes.Add;

public class AddCourseEpisodeCommand:IBaseCommand
{
    public Guid CourseId { get; set; }
    public Guid SectionId { get; set; }
    public string Title { get; set; }
    public string EnglishTitle { get; set; }
    public TimeSpan TimeSpan { get; set; }
    public IFormFile VideoFile { get; set; }
    public IFormFile? AttachmentFile { get; set; }
    public bool IsActive { get; set; }
}

class AddCourseEpisodeCommandHandler:IBaseCommandHandler<AddCourseEpisodeCommand>
{
    private readonly IFtpFileService _ftpFileService;
    private readonly ILocalFileService _localFileService;
    private readonly ICourseRepository _repository;

    public AddCourseEpisodeCommandHandler(ICourseRepository repository, IFtpFileService fileService, ILocalFileService localFileService)
    {
        _repository = repository;
        _localFileService = localFileService;
        _ftpFileService = fileService;
    }

    public async Task<OperationResult> Handle(AddCourseEpisodeCommand request, CancellationToken cancellationToken)
    {
        var course = await _repository.GetTracking(request.CourseId);
        if (course == null)
        {
            return OperationResult.NotFound();
        }

        string attExName = null;
        if (request.AttachmentFile is not null && request.AttachmentFile.IsValidCompressFile())
        {
            attExName = Path.GetExtension(request.AttachmentFile.FileName);
        }

        var episode= course.AddEpisode(request.SectionId,attExName,Path.GetExtension(request.VideoFile.FileName)
            ,request.TimeSpan, Guid.NewGuid(), request.Title,request.IsActive,request.EnglishTitle.ToSlug());

        await SaveFiles(request,episode);
        await _repository.Save();
        return OperationResult.Success();
    }

    private async Task SaveFiles(AddCourseEpisodeCommand request,Episode episode)
    {
        await _localFileService.SaveFile(request.VideoFile,
            CoreModuleDirectories.CourseEpisode(request.CourseId, episode.Token), episode.VideoName);

        if (request.AttachmentFile is not null)
        {
            if (request.AttachmentFile.IsValidCompressFile() is true)
            {
                await _localFileService.SaveFile(request.AttachmentFile,
                    CoreModuleDirectories.CourseEpisode(request.CourseId, episode.Token), episode.AttachmentName!);
            }
        }
    }
}