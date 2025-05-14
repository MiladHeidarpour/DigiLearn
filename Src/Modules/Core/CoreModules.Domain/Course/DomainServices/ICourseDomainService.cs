namespace CoreModules.Domain.Course.DomainServices;

public interface ICourseDomainService
{
    bool IsSlugExist(string slug);
}