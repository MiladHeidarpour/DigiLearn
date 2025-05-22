using System.ComponentModel.DataAnnotations;
using CoreModule.Application.TeacherAgg.Register;
using CoreModule.Domain.TeacherAgg.Enums;
using CoreModule.Facade.TeacherAgg;
using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigiLearn.Web.Pages.Profile;

[BindProperties]
public class RegisterTeacherModel : BaseRazor
{
    private readonly ITeacherFacade _facade;

    public RegisterTeacherModel(ITeacherFacade facade)
    {
        _facade = facade;
    }

    [Display(Name = "نام کاربری")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string UserName { get; set; }

    [Display(Name = "رزومه (پسومد بهتر است Pdf باشد)")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public IFormFile CvFile { get; set; }

    public async Task<IActionResult> OnGet()
    {
        var teacher = await _facade.GetByUserId(User.GetUserId());
        if (teacher is not null)
        {
            if (teacher.Status is TeacherStatus.Active or TeacherStatus.Inactive)
            {
                ErrorAlert("شما قبلا ثبت نام کرده اید");
            }
            else
            {
                ErrorAlert("درخواست شما در حال برسی است.لطفا منتظر بمانید");
            }
            return RedirectToPage("Index");
        }
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        var result = await _facade.Register(new RegisterTeacherCommand
        {
            UserId = User.GetUserId(),
            UserName = UserName,
            CvFile = CvFile
        });
        return RedirectAndShowAlert(result, RedirectToPage("Index"));
    }
}