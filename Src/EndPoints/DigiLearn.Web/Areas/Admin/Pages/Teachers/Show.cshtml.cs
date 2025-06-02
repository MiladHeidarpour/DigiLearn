using CoreModule.Application.TeacherAgg.AcceptRequest;
using CoreModule.Application.TeacherAgg.RejectRequest;
using CoreModule.Facade.TeacherAgg;
using CoreModule.Query.TeacherAgg._Dtos;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigiLearn.Web.Areas.Admin.Pages.Teachers;

public class ShowModel : BaseRazor
{
    private readonly ITeacherFacade _teacherFacade;

    public ShowModel(ITeacherFacade teacherFacade)
    {
        _teacherFacade = teacherFacade;
    }

    public TeacherDto Teacher { get; set; }
    public async Task<IActionResult> OnGet(Guid id)
    {
        var teacher = await _teacherFacade.GetById(id);
        if (teacher == null)
            return RedirectToPage("Index");

        Teacher = teacher;
        return Page();
    }

    public async Task<IActionResult> OnPostAccept(Guid id)
    {
        return await AjaxTryCatch(() => _teacherFacade.AcceptRequest(new AcceptTeacherRequestCommand(id)));
    }

    public async Task<IActionResult> OnPostReject(Guid id, string description)
    {

        var result = await _teacherFacade.RejectRequest(new RejectTeacherRequestCommand(id, description));
        return RedirectAndShowAlert(result, RedirectToPage("Show", new { id }));
    }
}