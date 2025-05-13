using Common.Domain;

namespace CoreModules.Domain.Course.Entities;

public class Section:BaseEntity
{
    public string Title { get;private set; }
    public int DisplayOrder { get;private set; }
    public IEnumerable<Episode> Episodes { get; }
}