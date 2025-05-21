using Common.Domain;
using Common.Domain.Exceptions;

namespace CoreModule.Domain.CourseAgg.Models;

public class Section : BaseEntity
{
    public Section(string title, int displayOrder, Guid courseId)
    {
        NullOrEmptyDomainDataException.CheckString(title,nameof(title));
        Title = title;
        DisplayOrder = displayOrder;
        CourseId = courseId;
        Episodes = new List<Episode>();
    }

    public Guid CourseId { get; private set; }
    public string Title { get; private set; }
    public int DisplayOrder { get; private set; }
    public IEnumerable<Episode> Episodes { get; private set; }


    public void Edit(string title, int displayOrder)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        Title = title;
        DisplayOrder = displayOrder;
    }

    public void AddEpisode(Guid token, string title, TimeSpan timeSpan, string videoName, string? attachmentName, bool isActive, string englishTitle)
    {
        Episodes = Episodes.Append(new Episode(token, title, timeSpan, videoName, attachmentName, isActive, Id,englishTitle));
    }
}