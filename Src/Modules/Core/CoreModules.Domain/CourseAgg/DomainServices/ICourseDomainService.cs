namespace CoreModule.Domain.CourseAgg.DomainServices;

public interface ICourseDomainService
{
    bool IsSlugExist(string slug);
}