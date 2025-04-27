using System.ComponentModel.DataAnnotations;
using Common.Application;
using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserModule.Core.Commands.Users.Edit;
using UserModule.Core.Services;

namespace DigiLearn.Web.Pages.Profile;

[BindProperties]
public class EditModel : BaseRazor
{
    private readonly IUserFacade _userFacade;

    public EditModel(IUserFacade userFacade)
    {
        _userFacade = userFacade;
    }

    [Required(ErrorMessage = "{0} را وارد کنید")]
    [Display(Name = "نام")]
    public string Name { get; set; }


    [Required(ErrorMessage = "{0} را وارد کنید")]
    [Display(Name = "نام خانوادگی")]
    public string Family { get; set; }


    [Required(ErrorMessage = "{0} را وارد کنید")]
    [Display(Name = "ایمیل")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }


    public async Task<IActionResult> OnGet()
    {
        var user = await _userFacade.GetUserByPhoneNumber(User.GetUserPhoneNumber());

        if (user is null)
            return RedirectAndShowAlert(OperationResult.NotFound("کاربری یافت نشد"), RedirectToPage("Index"));

        Name = user.Name;
        Family = user.Family;
        Email = user.Email;
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var result = await _userFacade.EditUserProfile(new EditUserCommand()
        {
            UserId = User.GetUserId(),
            Name = Name,
            Family = Family,
            Email = Email,
        });
        return RedirectAndShowAlert(result, RedirectToPage("Index"));
    }
}