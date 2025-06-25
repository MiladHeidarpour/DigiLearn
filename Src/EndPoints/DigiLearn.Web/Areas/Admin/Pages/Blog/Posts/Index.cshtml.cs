using BlogModule.Services;
using BlogModule.Services.Dtos.Commands;
using BlogModule.Services.Dtos.Queries;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using DigiLearn.Web.Infrastructure;
using UserModule.Core.Services;


namespace DigiLearn.Web.Areas.Admin.Pages.Blog.Posts;

public class IndexModel : BaseRazorFilter<BlogPostFilterParams>
{
    private readonly IBlogService _blogService;
    private readonly IUserFacade _userFacade;

    public IndexModel(IBlogService blogService, IUserFacade userFacade)
    {
        _blogService = blogService;
        _userFacade = userFacade;
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
}