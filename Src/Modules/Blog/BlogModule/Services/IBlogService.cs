using BlogModule.Services.Dtos.Commands;
using BlogModule.Services.Dtos.Queries;
using Common.Application;

namespace BlogModule.Services;

public interface IBlogService
{
    //Command
    Task<OperationResult> CreateCategory(CreateCategoryCommand command);
    Task<OperationResult> EditCategory(EditCategoryCommand command);
    Task<OperationResult> DeleteCategory(Guid categoryId);



    // Query
    Task<List<BlogCategoryDto>> GetAllCategories();
    Task<BlogCategoryDto> GetCategoryById(Guid categoryId);
}