using Common.Application;
using Common.Application.FileUtil;
using Common.Application.FileUtil.Interfaces;
using Common.Application.SecurityUtil;
using Common.Domain.ValueObjects;
using CoreModule.Application._Utilities;
using CoreModule.Application.CourseAgg.Create;
using CoreModule.Domain.CourseAgg.DomainServices;
using CoreModule.Domain.CourseAgg.Enums;
using CoreModule.Domain.CourseAgg.Models;
using CoreModule.Domain.CourseAgg.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CoreModule.Application.CourseAgg.Edit;

public class EditCourseCommand : IBaseCommand
{
    public Guid CourseId { get; set; }
    public Guid CategoryId { get; set; }
    public Guid SubCategoryId { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public string Description { get; set; }
    public IFormFile? ImageFile { get; set; }
    public IFormFile? VideoFile { get; set; }
    public int Price { get; set; }
    public CourseLevel CourseLevel { get; set; }
    public CourseStatus CourseStatus { get; set; }
    public SeoData SeoData { get; set; }
}

public class EditCourseCommandHandler : IBaseCommandHandler<EditCourseCommand>
{
    private readonly IFtpFileService _ftpFileService;
    private readonly ILocalFileService _localFileService;
    private readonly ICourseRepository _repository;
    private readonly ICourseDomainService _domainService;

    public EditCourseCommandHandler(ICourseRepository repository, ICourseDomainService domainService, IFtpFileService ftpFileService, ILocalFileService localFileService)
    {
        _repository = repository;
        _domainService = domainService;
        _ftpFileService = ftpFileService;
        _localFileService = localFileService;
    }

    public async Task<OperationResult> Handle(EditCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _repository.GetTracking(request.CourseId);
        if (course == null)
        {
            return OperationResult.NotFound();
        }
        var imageName = course.ImageName;
        var videoPath = course.VideoName;

        var oldVideoFileName = course.VideoName;
        var oldImageFileName = course.ImageName;

        if (request.VideoFile is not null)
        {
            if (request.VideoFile.IsValidMp4File() is false)
            {
                return OperationResult.Error("فایل وارد شده نامعتبر است");
            }

            videoPath = await _ftpFileService.SaveFileAndGenerateName(request.VideoFile, CoreModuleDirectories.CourseDemo(course.Id));
        }
        if (request.ImageFile is not null)
        {
            if (request.ImageFile.IsImage() is false)
            {
                return OperationResult.Error("فایل وارد شده نامعتبر است");
            }

            imageName = await _localFileService.SaveFileAndGenerateName(request.ImageFile, CoreModuleDirectories.CourseImage);
        }


        course.Edit(request.Title, request.Description, imageName, videoPath, request.Price, request.SeoData, request.CourseLevel
            , request.CourseStatus, request.CategoryId, request.SubCategoryId, request.Slug, _domainService);

        await _repository.Save();

        await DeleteOldFile(oldImageFileName, oldVideoFileName, request.VideoFile != null, request.ImageFile != null, course);
        return OperationResult.Success();
    }

    async Task DeleteOldFile(string image, string? video, bool isUploadNewVideo, bool isUploadNewImage, Course course)
    {
        if (isUploadNewVideo && string.IsNullOrWhiteSpace(video) == false)
        {
            await _ftpFileService.DeleteFile(CoreModuleDirectories.CourseDemo(course.Id), video);
        }

        if (isUploadNewImage)
        {
            _localFileService.DeleteFile(CoreModuleDirectories.CourseImage, image);
        }
    }
}

public class EditCourseCommandValidator : AbstractValidator<EditCourseCommand>
{
    public EditCourseCommandValidator()
    {
        RuleFor(r => r.Title)
            .NotNull()
            .NotEmpty();

        RuleFor(r => r.Slug)
            .NotNull()
            .NotEmpty();

        RuleFor(r => r.Description)
            .NotNull()
            .NotEmpty();

        RuleFor(r => r.ImageFile)
            .NotNull();
    }
}