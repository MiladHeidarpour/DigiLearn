using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Common.Application.SecurityUtil;
using DigiLearn.Web.Infrastructure.JwtUtil;
using UserModule.Core.Services;

namespace DigiLearn.Web.Pages.Auth;

[BindProperties]
public class LoginModel : BaseRazor
{
    private readonly IUserFacade _userFacade;
    private readonly IConfiguration _configuration;

    public LoginModel(IUserFacade userFacade, IConfiguration configuration)
    {
        _userFacade = userFacade;
        _configuration = configuration;
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

    public bool IsRememberMe { get; set; }
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        var user = await _userFacade.GetUserByPhoneNumber(PhoneNumber);
        if (user is null)
        {
            ErrorAlert("کاربری با مشخصات وارد شده یافت نشد");
            return Page();
        }

        var isComparePassword = Sha256Hasher.IsCompare(user.Password,Password);
        if (isComparePassword is false)
        {
            ErrorAlert("کاربری با مشخصات وارد شده یافت نشد");
            return Page();
        }

        var token = JwtTokenBuilder.BuildToken(user, _configuration);
        if (IsRememberMe is true)
        {
            HttpContext.Response.Cookies.Append("Token", token, new CookieOptions()
            {
                HttpOnly = true,
                Expires = DateTimeOffset.Now.AddDays(30),
                Secure = true,
            });
        }
        else
        {
            HttpContext.Response.Cookies.Append("Token", token, new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
            });
        }

        return RedirectToPage("../Index");
    }
}