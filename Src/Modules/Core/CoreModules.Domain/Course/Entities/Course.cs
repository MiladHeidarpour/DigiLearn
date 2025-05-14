using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;
using CoreModules.Domain.Course.Enums;

namespace CoreModules.Domain.Course.Entities;

public class Course : BaseEntity
{
    public Course(Guid teacherId, string title, string description, string imageName, string? videoName, int price, CourseLevel courseLevel, SeoData seoData)
    {
        Guard(title, description, imageName);
        TeacherId = teacherId;
        Title = title;
        Description = description;
        ImageName = imageName;
        VideoName = videoName;
        Price = price;
        LastUpdate = DateTime.Now;
        CourseLevel = courseLevel;
        SeoData = seoData;
        CourseStatus = CourseStatus.StartSoon;
        Sections = new();
    }

    public Guid TeacherId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string ImageName { get; private set; }
    public string? VideoName { get; private set; }
    public int Price { get; private set; }
    public DateTime LastUpdate { get; private set; }
    public CourseLevel CourseLevel { get; set; }
    public CourseStatus CourseStatus { get; set; }
    public SeoData SeoData { get; private set; }

    public List<Section> Sections { get; private set; }


    public void AddSection(string title, int displayOrder)
    {
        if (Sections.Any(f=>f.Title==title))
        {
            throw new InvalidDomainDataException("Title is Exist");
        }

        Sections.Add(new Section(title, displayOrder,Id));
    }

    public void RemoveSection(Guid sectionId)
    {
        var section = Sections.FirstOrDefault(f => f.Id == sectionId);

        if (section == null)
            throw new InvalidDomainDataException("Section NotFound");

        Sections.Remove(section);
    }

    public void AddEpisode(Guid sectionId,Guid token, string title, TimeSpan timeSpan, string videoExtension, string? attachmentExtension, bool isActive, string englishTitle)
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

        section.AddEpisode(token,title,timeSpan,vidName,attName,isActive,englishTitle);
    }

    public void Guard(string title, string description, string imageName)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        NullOrEmptyDomainDataException.CheckString(description, nameof(description));
        NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
    }
}