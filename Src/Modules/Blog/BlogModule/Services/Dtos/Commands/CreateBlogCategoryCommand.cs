namespace BlogModule.Services.Dtos.Commands;

public class CreateBlogCategoryCommand
{
    public string Title { get; set; }
    public string Slug { get; set; }
}