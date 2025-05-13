using Common.Domain;

namespace CoreModules.Domain.Course.Entities;

public class Episode:BaseEntity
{
    public Episode(Guid token, string title, TimeSpan timeSpan, string videoName, string attachmentName, bool isActive)
    {
        Token = token;
        Title = title;
        TimeSpan = timeSpan;
        VideoName = videoName;
        AttachmentName = attachmentName;
        IsActive = isActive;
    }

    public Guid Token { get;private set; }
    public string Title { get;private set; }
    public TimeSpan TimeSpan { get;private set; }
    public string VideoName { get;private set; }
    public string AttachmentName { get;private set; }
    public bool IsActive { get;private set; }
}