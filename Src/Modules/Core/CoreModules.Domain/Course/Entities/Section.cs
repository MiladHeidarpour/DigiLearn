using Common.Domain;

namespace CoreModules.Domain.Course.Entities;

public class Section : BaseEntity
{
    public Section(string title, int displayOrder, Guid courseId)
    {
        Title = title;
        DisplayOrder = displayOrder;
        CourseId = courseId;
        Episodes = new List<Episode>();
    }

    public Guid CourseId { get; private set; }
    public string Title { get; private set; }
    public int DisplayOrder { get; private set; }
    public IEnumerable<Episode> Episodes { get; private set; }

    public void AddEpisode(Guid token, string title, TimeSpan timeSpan, string videoName, string? attachmentName, bool isActive, string englishTitle)
    {
        Episodes = Episodes.Append(new Episode(token, title, timeSpan, videoName, attachmentName, isActive, Id,englishTitle));
    }
}