using CoreModule.Facade.TeacherAgg;
using CoreModule.Query.TeacherAgg._Dtos;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigiLearn.Web.Areas.Admin.Pages.Teachers;

public class IndexModel : BaseRazor
{
    private readonly ITeacherFacade _teacherFacade;

    public IndexModel(ITeacherFacade teacherFacade)
    {
        _teacherFacade = teacherFacade;
    }

    public List<TeacherDto> Teachers { get; set; }
    public async Task OnGet()
    {
        Teachers = await _teacherFacade.GetLis();
    }

    public async Task<IActionResult> OnPostToggleStatus(Guid id)
    {
        return await AjaxTryCatch(() => _teacherFacade.ToggleStatus(id));
    }
}