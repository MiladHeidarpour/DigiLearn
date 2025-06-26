using BlogModule.Services.Dtos.Commands;
using BlogModule.Services.Dtos.Queries;
using Common.Application;

namespace BlogModule.Services;

public interface IBlogService
{
    //Category Command
    Task<OperationResult> CreateCategory(CreateBlogCategoryCommand command);
    Task<OperationResult> EditCategory(EditBlogCategoryCommand command);
    Task<OperationResult> DeleteCategory(Guid categoryId);


    //Post Command
    Task<OperationResult> CreatePost(CreatePostCommand command);
    Task<OperationResult> EditPost(EditPostCommand command);
    Task<OperationResult> DeletePost(Guid postId);
    Task AddPostVisit(Guid id);


    //Category Query
    Task<List<BlogCategoryDto>> GetAllCategories();
    Task<BlogCategoryDto> GetCategoryById(Guid categoryId);

    //Post Query

    Task<BlogPostDto?> GetPostById(Guid postId);
    Task<BlogPostFilterItemDto?> GetPostBySlug(string slug);
    Task<BlogPostFilterResult> GetPostByFilter(BlogPostFilterParams filterParams);
}