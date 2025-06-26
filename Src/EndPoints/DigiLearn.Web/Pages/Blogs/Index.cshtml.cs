using BlogModule.Services;
using BlogModule.Services.Dtos.Queries;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigiLearn.Web.Pages.Blogs;

public class IndexModel : BaseRazorFilter<BlogPostFilterParams>
{
    private readonly IBlogService _blogService;

    public IndexModel(IBlogService blogService)
    {
        _blogService = blogService;
    }

    public List<BlogCategoryDto> Categories { get; set; }
    public BlogPostFilterResult FilterResult { get; set; }
    public async Task OnGet()
    {
        FilterResult = await _blogService.GetPostByFilter(FilterParams);
        Categories = await _blogService.GetAllCategories();
    }
}