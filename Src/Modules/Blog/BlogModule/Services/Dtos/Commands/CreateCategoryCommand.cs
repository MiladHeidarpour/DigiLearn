namespace BlogModule.Services.Dtos.Commands;

public class CreateCategoryCommand
{
    public string Title { get; set; }
    public string Slug { get; set; }
}