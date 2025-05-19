using Common.Application;
using CoreModules.Domain.CategoryAgg.DomainServices;
using CoreModules.Domain.CategoryAgg.Models;
using CoreModules.Domain.CategoryAgg.Repositories;
using FluentValidation;

namespace CoreModule.Application.CategoryAgg.Create;

public class CreateCategoryCommand:IBaseCommand
{
    public string Title { get; set; }
    public string Slug { get; set; }
}

public class CreateCategoryCommandHandler:IBaseCommandHandler<CreateCategoryCommand>
{
    private readonly ICourseCategoryRepository _repository;
    private readonly ICategoryDomainService _domainService;

    public CreateCategoryCommandHandler(ICourseCategoryRepository repository, ICategoryDomainService domainService)
    {
        _repository = repository;
        _domainService = domainService;
    }

    public async Task<OperationResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new CourseCategory(request.Title, request.Slug, null, _domainService);

        _repository.Add(category);
        await _repository.Save();
        return OperationResult.Success();
    }
}

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(r => r.Title)
            .NotNull()
            .NotEmpty();

        RuleFor(r => r.Slug)
            .NotNull()
            .NotEmpty();
    }
}
