using Common.Application;
using CoreModule.Domain.CategoryAgg.DomainServices;
using CoreModule.Domain.CategoryAgg.Models;
using CoreModule.Domain.CategoryAgg.Repositories;
using FluentValidation;

namespace CoreModule.Application.CategoryAgg.AddChild;

public class AddCategoryChildCommand:IBaseCommand
{
    public Guid ParentId { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
}

public class AddCategoryChildCommandHandler:IBaseCommandHandler<AddCategoryChildCommand>
{
    private readonly ICourseCategoryRepository _repository;
    private readonly ICategoryDomainService _domainService;

    public AddCategoryChildCommandHandler(ICourseCategoryRepository repository, ICategoryDomainService domainService)
    {
        _repository = repository;
        _domainService = domainService;
    }

    public async Task<OperationResult> Handle(AddCategoryChildCommand request, CancellationToken cancellationToken)
    {
        var category = new CourseCategory(request.Title, request.Slug, request.ParentId, _domainService);

        _repository.Add(category);
        await _repository.Save();
        return OperationResult.Success();
    }
}

public class AddCategoryChildCommandValidator : AbstractValidator<AddCategoryChildCommand>
{
    public AddCategoryChildCommandValidator()
    {
        RuleFor(r => r.ParentId)
            .NotNull()
            .NotEmpty();

        RuleFor(r => r.Title)
            .NotNull()
            .NotEmpty();

        RuleFor(r => r.Slug)
            .NotNull()
            .NotEmpty();
    }
}