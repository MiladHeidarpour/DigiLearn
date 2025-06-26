using BlogModule.Services;
using BlogModule.Services.Dtos.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigiLearn.Web.Pages.Blogs;

public class ShowModel : PageModel
{
    private readonly IBlogService _blogService;

    public ShowModel(IBlogService blogService)
    {
        _blogService = blogService;
    }

    public BlogPostFilterItemDto BlogPost { get; set; }
    public async Task<IActionResult> OnGet(string slug)
    {
        var article = await _blogService.GetPostBySlug(slug);
        if (article == null)
        {
            return NotFound();
        }
        BlogPost = article;
        _blogService.AddPostVisit(article.Id);
        return Page();
    }
}