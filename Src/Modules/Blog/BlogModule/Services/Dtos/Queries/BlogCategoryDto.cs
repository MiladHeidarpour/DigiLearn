namespace BlogModule.Services.Dtos.Queries;

public class BlogCategoryDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public DateTime CreationDate { get; set; }
}