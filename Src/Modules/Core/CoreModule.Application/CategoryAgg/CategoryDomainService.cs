using Common.Domain.Utils;
using CoreModule.Domain.CategoryAgg.DomainServices;
using CoreModule.Domain.CategoryAgg.Repositories;

namespace CoreModule.Application.CategoryAgg;

public class CategoryDomainService:ICategoryDomainService
{
    private readonly ICourseCategoryRepository _repository;

    public CategoryDomainService(ICourseCategoryRepository repository)
    {
        _repository = repository;
    }

    public bool IsSlugExist(string slug)
    {
        return _repository.Exists(f => f.Slug == slug.ToSlug());
    }
}