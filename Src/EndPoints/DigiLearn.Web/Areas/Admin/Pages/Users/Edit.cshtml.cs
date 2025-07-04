using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using UserModule.Core.Commands.Users.FullEdit;
using UserModule.Core.Services;

namespace DigiLearn.Web.Areas.Admin.Pages.Users;

[BindProperties]
public class EditModel : BaseRazor
{
    private readonly IUserFacade _userFacade;

    public EditModel(IUserFacade userFacade)
    {
        _userFacade = userFacade;
    }

    [Display(Name = "نام")]
    public string? Name { get; set; }
    [Display(Name = "نام خانوادگی")]

    public string? Family { get; set; }

    [Display(Name = "ایمیل")]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    [Display(Name = "شماره تلفن")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string PhoneNumber { get; set; }


    [Display(Name = "کلمه عبور")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    public List<Guid> CurrentUserRoles { get; set; }

    public async Task<IActionResult> OnGet(Guid id)
    {
        var user = await _userFacade.GetUserById(id);
        if (user == null)
        {
            ErrorAlert("کاربر یافت نشد");
            return RedirectToPage("Index");
        }

        Name = user.Name;
        Family = user.Family;
        Email = user.Email;
        PhoneNumber = user.PhoneNumber;
        CurrentUserRoles = user.Roles.Select(s => s.Id).ToList();
        return Page();
    }

    public async Task<IActionResult> OnPost(Guid id, string[] roles)
    {
        var res = await _userFacade.EditUser(new FullEditUserCommand
        {
            UserId = id,
            Name = Name,
            Family = Family,
            Email = Email,
            PhoneNumber = PhoneNumber,
            Password = Password,
            Roles = roles.Select(s => Guid.Parse(s)).ToList()
        });
        return RedirectAndShowAlert(res, RedirectToPage("Index"));
    }
}