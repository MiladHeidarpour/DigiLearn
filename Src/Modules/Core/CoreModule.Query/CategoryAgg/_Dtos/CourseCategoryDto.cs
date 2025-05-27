using Common.Query;

namespace CoreModule.Query.CategoryAgg._Dtos;

public class CourseCategoryDto:BaseDto
{
    public string Title { get; set; }
    public string Slug { get; set; }
    public Guid? ParentId { get; set; }
    public List<CourseCategoryChild> Children { get; set; }
}
public class CourseCategoryChild : BaseDto
{
    public string Title { get; set; }
    public string Slug { get; set; }
    public Guid? ParentId { get; set; }
}