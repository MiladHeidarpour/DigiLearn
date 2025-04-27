using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using DigiLearn.Web.Infrastructure;
using UserModule.Core.Commands.ChangePassword;
using UserModule.Core.Services;

namespace DigiLearn.Web.Pages.Profile;


[BindProperties]
public class ChangePasswordModel : BaseRazor
{
    private readonly IUserFacade _userFacade;

    public ChangePasswordModel(IUserFacade userFacade)
    {
        _userFacade = userFacade;
    }

    [Required(ErrorMessage = "{0} را وارد کنید")]
    [Display(Name = "پسورد فعلی")]
    [DataType(DataType.Password)]
    public string CurrentPassword { get; set; }


    [Required(ErrorMessage = "{0} را وارد کنید")]
    [Display(Name = "پسورد جدید")]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }


    [Required(ErrorMessage = "{0} را وارد کنید")]
    [Display(Name = "تکرار پسورد جدید")]
    [Compare("NewPassword", ErrorMessage = "کلمه های عبور یکسان نیستند")]
    [DataType(DataType.Password)]
    public string ConfirmNewPassword { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        var result = await _userFacade.ChangePassword(new ChangeUserPasswordCommand()
        {
            CurrentPassword = CurrentPassword,
            NewPassword = NewPassword,
            UserId = User.GetUserId(),
        });
        return RedirectAndShowAlert(result, RedirectToPage("index"));
    }
}