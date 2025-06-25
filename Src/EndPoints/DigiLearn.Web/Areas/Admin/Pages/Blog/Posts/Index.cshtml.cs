using BlogModule.Services;
using BlogModule.Services.Dtos.Commands;
using BlogModule.Services.Dtos.Queries;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Common.Application;
using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.ViewModels.Admins;
using UserModule.Core.Services;


namespace DigiLearn.Web.Areas.Admin.Pages.Blog.Posts;

public class IndexModel : BaseRazorFilter<BlogPostFilterParams>
{
    private readonly IBlogService _blogService;
    private readonly IUserFacade _userFacade;
    private readonly IRenderViewToString _renderView;
    public IndexModel(IBlogService blogService, IUserFacade userFacade, IRenderViewToString renderView)
    {
        _blogService = blogService;
        _userFacade = userFacade;
        _renderView = renderView;
    }


    public List<BlogCategoryDto> Categories { get; set; }
    public BlogPostFilterResult FilterResult { get; set; }



    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [BindProperty]
    public string Title { get; set; }

    [Display(Name = "نام نویسنده")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [BindProperty]
    public string OwnerName { get; set; }

    [Display(Name = "توضیحات")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [UIHint("Ckeditor4")]
    [BindProperty]
    public string Description { get; set; }

    [Display(Name = "slug")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [BindProperty]
    public string Slug { get; set; }

    [Display(Name = "عکس مقاله")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [BindProperty]
    public IFormFile ImageFile { get; set; }


    [Display(Name = "دسته بندی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [BindProperty]
    public Guid CategoryId { get; set; }

    public async Task OnGet()
    {
        Categories = await _blogService.GetAllCategories();
        FilterResult = await _blogService.GetPostByFilter(FilterParams);

        var user = await _userFacade.GetUserByPhoneNumber(User.GetUserPhoneNumber());
        if (user != null)
        {
            OwnerName = user.Name + " " + user.Family;
        }
    }

    public async Task<IActionResult> OnPost()
    {
        var result = await _blogService.CreatePost(new CreatePostCommand()
        {
            UserId = User.GetUserId(),
            OwnerName = OwnerName,
            CategoryId = CategoryId,
            Description = Description,
            Slug = Slug,
            Title = Title,
            ImageFile = ImageFile
        });
        return RedirectAndShowAlert(result, RedirectToPage("Index"));
    }

    public async Task<IActionResult> OnGetShowEditPage(Guid id)
    {
        return await AjaxTryCatch(async () =>
        {
            var post = await _blogService.GetPostById(id);
            if (post is null)
            {
                return OperationResult<string>.NotFound();
            }

            var categories = await _blogService.GetAllCategories();
            var viewModel = new EditPostViewModel
            {
                Id = post.Id,
                CategoryId = post.CategoryId,
                Title = post.Title,
                UserId = post.UserId,
                OwnerName = post.OwnerName,
                Description = post.Description,
                Slug = post.Slug,
                //ImageFile = post.ImageName,
                Categories = categories
            };

            var view = await _renderView.RenderToStringAsync("_Edit", viewModel, PageContext);

            return OperationResult<string>.Success(view);
        });
    }

    public async Task<IActionResult> OnPostEdit(EditPostViewModel viewModel)
    {
        return await AjaxTryCatch(async () => await _blogService.EditPost(new EditPostCommand
        {
            Id = viewModel.Id,
            CategoryId = viewModel.CategoryId,
            Title = viewModel.Title,
            OwnerName = viewModel.OwnerName,
            Description = viewModel.Description,
            Slug = viewModel.Slug,
            ImageFile = viewModel.ImageFile
        }));
    }

    public async Task<IActionResult> OnPostDelete(Guid id)
    {
        return await AjaxTryCatch(() => _blogService.DeletePost(id));
    }
}