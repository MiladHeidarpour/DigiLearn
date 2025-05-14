using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.Utils;
using CoreModules.Domain.Category.DomainServices;

namespace CoreModules.Domain.Category.Models;

public class CourseCategory:BaseEntity
{
    public CourseCategory(string title, string slug, Guid? parentId, ICategoryDomainService domainService)
    {
        Guard(title,slug);
        if (domainService.IsSlugExist(slug))
        {
            throw new InvalidDomainDataException("Slug Is Exist");
        }

        Title = title;
        Slug = slug;
        ParentId = parentId;
    }

    public string Title { get;private set; }
    public string Slug { get;private set; }
    public Guid? ParentId { get;private set; }

    public void Edit(string title,string slug,ICategoryDomainService domainService)
    {
        Guard(title,slug);

        if (slug!=Slug)
        {
            if (domainService.IsSlugExist(slug))
            {
                throw new InvalidDomainDataException("Slug Is Exist");
            }
        }
        Title = title;
        Slug = slug;
    }

    void Guard(string title,string slug)
    {
        NullOrEmptyDomainDataException.CheckString(title,nameof(title));
        NullOrEmptyDomainDataException.CheckString(slug,nameof(slug));

        if (slug.IsUniCode())
        {
            throw new InvalidDomainDataException("Slug Is Invalid");
        }
    }
}

