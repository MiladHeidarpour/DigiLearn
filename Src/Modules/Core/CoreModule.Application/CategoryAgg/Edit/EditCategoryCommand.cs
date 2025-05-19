using Common.Application;
using CoreModules.Domain.CategoryAgg.DomainServices;
using CoreModules.Domain.CategoryAgg.Repositories;
using FluentValidation;

namespace CoreModule.Application.CategoryAgg.Edit;

public class EditCategoryCommand : IBaseCommand
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
}


public class EditCategoryCommandHandler : IBaseCommandHandler<EditCategoryCommand>
{
    private readonly ICourseCategoryRepository _repository;
    private readonly ICategoryDomainService _domainService;

    public EditCategoryCommandHandler(ICourseCategoryRepository repository, ICategoryDomainService domainService)
    {
        _repository = repository;
        _domainService = domainService;
    }

    public async Task<OperationResult> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetTracking(request.Id);
        if (category is null)
        {
            return OperationResult.NotFound();
        }

        category.Edit(request.Title, request.Slug, _domainService);
        await _repository.Save();
        return OperationResult.Success();
    }
}

public class EditCategoryCommandValidator : AbstractValidator<EditCategoryCommand>
{
    public EditCategoryCommandValidator()
    {
        RuleFor(r => r.Title)
            .NotNull()
            .NotEmpty();

        RuleFor(r => r.Slug)
            .NotNull()
            .NotEmpty();
    }
}
