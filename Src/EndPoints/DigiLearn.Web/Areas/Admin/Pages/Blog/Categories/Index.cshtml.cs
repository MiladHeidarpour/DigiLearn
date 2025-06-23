using System.ComponentModel.DataAnnotations;
using BlogModule.Services;
using BlogModule.Services.Dtos.Commands;
using BlogModule.Services.Dtos.Queries;
using Common.Application;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigiLearn.Web.Areas.Admin.Pages.Blog.Categories;

public class IndexModel : BaseRazor
{
    private readonly IBlogService _service;
    private readonly IRenderViewToString _renderView;

    public IndexModel(IBlogService service, IRenderViewToString renderView)
    {
        _service = service;
        _renderView = renderView;
    }


    [BindProperty]
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; }

    [BindProperty]
    [Display(Name = "اسلاگ")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Slug { get; set; }


    public List<BlogCategoryDto> Categories { get; set; }
    public async Task OnGet()
    {
        Categories = await _service.GetAllCategories();
    }

    public async Task<IActionResult> OnGetShowEditPage(Guid id)
    {
        return await AjaxTryCatch(async () =>
        {
            var category = await _service.GetCategoryById(id);
            if (category == null)
            {
                return OperationResult<string>.NotFound();
            }

            var viewResult = await _renderView.RenderToStringAsync("_Edit", new EditBlogCategoryCommand()
            {
                Title = category.Title,
                Slug = category.Slug,
                Id = id
            }, PageContext);

            return OperationResult<string>.Success(viewResult);
        });
    }

    public async Task<IActionResult> OnPostDelete(Guid id)
    {
        return await AjaxTryCatch(() => _service.DeleteCategory(id));
    }
    public async Task<IActionResult> OnPostEdit(EditBlogCategoryCommand command)
    {
        return await AjaxTryCatch(() => _service.EditCategory(command));
    }

    public async Task<IActionResult> OnPost()
    {
        return await AjaxTryCatch(() => _service.CreateCategory(new CreateBlogCategoryCommand()
        {
            Title = Title,
            Slug = Slug
        }));
    }
}