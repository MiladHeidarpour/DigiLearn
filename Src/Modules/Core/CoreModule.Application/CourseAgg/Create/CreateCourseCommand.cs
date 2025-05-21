using Common.Application;
using Common.Application.FileUtil;
using Common.Application.FileUtil.Interfaces;
using Common.Domain.ValueObjects;
using CoreModule.Application._Utilities;
using CoreModule.Domain.CourseAgg.DomainServices;
using CoreModule.Domain.CourseAgg.Enums;
using CoreModule.Domain.CourseAgg.Models;
using CoreModule.Domain.CourseAgg.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CoreModule.Application.CourseAgg.Create;

public class CreateCourseCommand : IBaseCommand
{
    public Guid TeacherId { get; set; }
    public Guid CategoryId { get; set; }
    public Guid SubCategoryId { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public string Description { get; set; }
    public IFormFile ImageFile { get; set; }
    public IFormFile? VideoFile { get; set; }
    public int Price { get; set; }
    public CourseLevel CourseLevel { get; set; }
    public SeoData SeoData { get; set; }
}

class CreateCourseCommandHandle : IBaseCommandHandler<CreateCourseCommand>
{
    private readonly IFtpFileService _ftpFileService;
    private readonly ILocalFileService _localFileService;
    private readonly ICourseRepository _repository;
    private readonly ICourseDomainService _domainService;

    public CreateCourseCommandHandle(ICourseRepository repository, ICourseDomainService domainService, IFtpFileService ftpFileService, ILocalFileService localFileService)
    {
        _repository = repository;
        _domainService = domainService;
        _ftpFileService = ftpFileService;
        _localFileService = localFileService;
    }

    public async Task<OperationResult> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        string videoPath = "";
        Guid courseId = Guid.NewGuid();
        if (request.VideoFile is not null)
        {
            if (request.VideoFile.IsValidMp4File() is false)
            {
                return OperationResult.Error("فایل وارد شده نامعتبر است");
            }

            videoPath = await _ftpFileService.SaveFileAndGenerateName(request.VideoFile, CoreModuleDirectories.CourseDemo(courseId));
        }

        var imageName = await _localFileService.SaveFileAndGenerateName(request.ImageFile, CoreModuleDirectories.CourseImage);
        var course = new Course(request.TeacherId, request.Title, request.Description, imageName, videoPath, request.Price,
            request.CourseLevel, request.SeoData, request.CategoryId, request.SubCategoryId, request.Slug, _domainService)
        {
            Id = courseId,
        };

        _repository.Add(course);
        await _repository.Save();
        return OperationResult.Success();
    }
}

public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator()
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