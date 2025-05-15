namespace CoreModules.Domain.CategoryAgg.DomainServices;

public interface ICategoryDomainService
{
    bool IsSlugExist(string slug);
}