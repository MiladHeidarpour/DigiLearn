using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;
using CoreModules.Domain.CourseAgg.DomainServices;
using CoreModules.Domain.CourseAgg.Enums;

namespace CoreModules.Domain.CourseAgg.Models;

public class Course : AggregateRoot
{
    public Course(Guid teacherId, string title, string description, string imageName, string? videoName, int price,
        CourseLevel courseLevel, SeoData seoData, Guid categoryId, Guid subCategoryId, string slug, ICourseDomainService domainService)
    {
        Guard(title, description, imageName, slug);
        if (domainService.IsSlugExist(slug))
        {
            throw new InvalidDomainDataException("Slug Is Exist");
        }
        TeacherId = teacherId;
        Title = title;
        Description = description;
        ImageName = imageName;
        VideoName = videoName;
        Price = price;
        LastUpdate = DateTime.Now;
        CourseLevel = courseLevel;
        SeoData = seoData;
        CategoryId = categoryId;
        SubCategoryId = subCategoryId;
        Slug = slug;
        CourseStatus = CourseStatus.StartSoon;
        Sections = new();
    }

    public Guid TeacherId { get; private set; }
    public Guid CategoryId { get; private set; }
    public Guid SubCategoryId { get; private set; }
    public string Title { get; private set; }
    public string Slug { get; private set; }
    public string Description { get; private set; }
    public string ImageName { get; private set; }
    public string? VideoName { get; private set; }
    public int Price { get; private set; }
    public DateTime LastUpdate { get; private set; }
    public CourseLevel CourseLevel { get; set; }
    public CourseStatus CourseStatus { get; set; }
    public SeoData SeoData { get; private set; }

    public List<Section> Sections { get; private set; }

    public void Edit(string title, string description, string imageName, string? videoName, int price,
        CourseLevel courseLevel, CourseStatus courseStatus, SeoData seoData, Guid categoryId, Guid subCategoryId, string slug, ICourseDomainService domainService)
    {
        Guard(title, description, imageName, slug);
        if (Slug != slug)
        {
            if (domainService.IsSlugExist(slug))
            {
                throw new InvalidDomainDataException("Slug Is Exist");
            }
        }

        Title = title;
        Description = description;
        ImageName = imageName;
        VideoName = videoName;
        Price = price;
        LastUpdate = DateTime.Now;
        CourseLevel = courseLevel;
        SeoData = seoData;
        CategoryId = categoryId;
        SubCategoryId = subCategoryId;
        Slug = slug;
        CourseStatus = courseStatus;
    }

    public void AddSection(string title, int displayOrder)
    {
        if (Sections.Any(f => f.Title == title))
        {
            throw new InvalidDomainDataException("Title is Exist");
        }

        Sections.Add(new Section(title, displayOrder, Id));
    }

    public void EditSection(Guid sectionId, string title, int displayOrder)
    {
        var section = Sections.FirstOrDefault(f => f.Id == sectionId);

        if (section == null)
            throw new InvalidDomainDataException("Section NotFound");

        section.Edit(title, displayOrder);
    }

    public void RemoveSection(Guid sectionId)
    {
        var section = Sections.FirstOrDefault(f => f.Id == sectionId);

        if (section == null)
            throw new InvalidDomainDataException("Section NotFound");

        Sections.Remove(section);
    }

    public void AddEpisode(Guid sectionId, Guid token, string title, TimeSpan timeSpan, string videoExtension, string? attachmentExtension, bool isActive, string englishTitle)
    {
        var section = Sections.FirstOrDefault(f => f.Id == sectionId);

        if (section == null)
            throw new InvalidDomainDataException("Section NotFound");

        var episodeCount = Sections.Sum(s => s.Episodes.Count());
        var episodeTitle = $"{episodeCount + 1}_{englishTitle}";

        string attName = null;
        if (string.IsNullOrWhiteSpace(attachmentExtension) is false)
        {
            attName = $"{episodeTitle}.{attachmentExtension}";
        }
        var vidName = $"{episodeTitle}.{videoExtension}";


        if (isActive is true)
        {
            LastUpdate = DateTime.Now;
            if (CourseStatus == CourseStatus.StartSoon)
            {
                CourseStatus = CourseStatus.InProgress;
            }
        }
        section.AddEpisode(token, title, timeSpan, vidName, attName, isActive, englishTitle);
    }

    public void AcceptEpisode(Guid episodeId)
    {
        var section = Sections.FirstOrDefault(f => f.Episodes.Any(e => e.Id == episodeId && e.IsActive == false));
        if (section is null)
        {
            throw new NullOrEmptyDomainDataException();
        }

        var episode = section.Episodes.First(f => f.Id == episodeId);

        episode.ToggleStatus();
        LastUpdate = DateTime.Now;
    }

    public void Guard(string title, string description, string imageName, string slug)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        NullOrEmptyDomainDataException.CheckString(slug, nameof(slug));
        NullOrEmptyDomainDataException.CheckString(description, nameof(description));
        NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
    }
}