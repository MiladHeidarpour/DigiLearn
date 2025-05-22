using Common.Domain.Utils;
using CoreModule.Domain.CourseAgg.DomainServices;
using CoreModule.Domain.CourseAgg.Repositories;

namespace CoreModule.Application.CourseAgg;

public class CourseDomainService : ICourseDomainService
{
    private readonly ICourseRepository _repository;

    public CourseDomainService(ICourseRepository repository)
    {
        _repository = repository;
    }

    public bool IsSlugExist(string slug)
    {
        return _repository.Exists(f => f.Slug == slug.ToSlug());
    }
}