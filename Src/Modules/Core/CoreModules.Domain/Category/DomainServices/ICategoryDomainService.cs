namespace CoreModules.Domain.Category.DomainServices;

public interface ICategoryDomainService
{
    bool IsSlugExist(string slug);
}