using System.ComponentModel.DataAnnotations;
using Common.Application;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserModule.Core.Commands.Users.Register;
using UserModule.Core.Services;

namespace DigiLearn.Web.Pages.Auth;

[BindProperties]
public class RegisterModel : BaseRazor
{
    private readonly IUserFacade _userFacade;

    public RegisterModel(IUserFacade userFacade)
    {
        _userFacade = userFacade;
    }

    [Required(ErrorMessage = "{0} را وارد کنید!")]
    [Display(Name = "شماره تلفن")]
    [MaxLength(11, ErrorMessage = "شماره تلفن باید 11 رقم باشد")]
    [MinLength(11, ErrorMessage = "شماره تلفن باید 11 رقم باشد")]
    public string PhoneNumber { get; set; }


    [Required(ErrorMessage = "{0} را وارد کنید!")]
    [Display(Name = "کلمه عبور")]
    [MinLength(5, ErrorMessage = "کلمه عبور باید بیشتر از 5 کارکتر باشد")]
    public string Password { get; set; }

    [Required(ErrorMessage = "{0} را وارد کنید!")]
    [Display(Name = "تکرار کلمه عبور")]
    [Compare("Password", ErrorMessage = "کلمه عبور یکسان نمیباشد")]
    public string ConfirmPassword { get; set; }
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        var result = await _userFacade.RegisterUser(new RegisterUserCommand()
        {
            PhoneNumber = PhoneNumber,
            Password = Password,
        });

        if (result.Status is OperationResultStatus.Success)
        {
            result.Message = "ثبت نام با موفقیت انجام شد";
            
        }
        return RedirectAndShowAlert(result, RedirectToPage("Login"));
    }
}