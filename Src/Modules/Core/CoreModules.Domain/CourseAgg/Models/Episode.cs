using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.Utils;

namespace CoreModule.Domain.CourseAgg.Models;

public class Episode:BaseEntity
{
    public Episode(Guid token, string title, TimeSpan timeSpan, string videoName, string? attachmentName, bool isActive, Guid sectionId, string englishTitle)
    {
        Guard(title,videoName,englishTitle);
        Token = token;
        Title = title;
        TimeSpan = timeSpan;
        VideoName = videoName;
        AttachmentName = attachmentName;
        IsActive = isActive;
        SectionId = sectionId;
        EnglishTitle = englishTitle;
    }

    public Guid SectionId { get;private set; }
    public Guid Token { get;private set; }
    public string Title { get;private set; }
    public string EnglishTitle { get;private set; }
    public TimeSpan TimeSpan { get;private set; }
    public string VideoName { get;private set; }
    public string? AttachmentName { get;private set; }
    public bool IsActive { get;private set; }


    public void ToggleStatus()
    {
        IsActive = !IsActive;
    }

    void Guard( string title, string videoName, string englishTitle)
    {
        NullOrEmptyDomainDataException.CheckString(title,nameof(title));
        NullOrEmptyDomainDataException.CheckString(videoName,nameof(videoName));
        NullOrEmptyDomainDataException.CheckString(englishTitle,nameof(englishTitle));

        if (englishTitle.IsUniCode())
        {
            throw new InvalidDomainDataException("Invalid EnglishTitle");
        }
    }
}