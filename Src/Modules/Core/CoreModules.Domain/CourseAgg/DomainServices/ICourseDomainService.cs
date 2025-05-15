namespace CoreModules.Domain.CourseAgg.DomainServices;

public interface ICourseDomainService
{
    bool IsSlugExist(string slug);
}