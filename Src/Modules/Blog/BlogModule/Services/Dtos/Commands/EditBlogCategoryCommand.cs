namespace BlogModule.Services.Dtos.Commands;

public class EditBlogCategoryCommand
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
}