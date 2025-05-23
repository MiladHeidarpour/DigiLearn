using System.Drawing.Printing;
using CoreModule.Facade.CourseAgg;
using CoreModule.Facade.TeacherAgg;
using CoreModule.Query.CourseAgg._Dtos;
using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigiLearn.Web.Pages.Profile.Teacher.Courses;

[ServiceFilter(typeof(TeacherActionFilter))]
public class IndexModel : BaseRazorFilter<CourseFilterParams>
{
    private readonly ICourseFacade _facade;
    private readonly ITeacherFacade _teacherFacade;

    public IndexModel(ICourseFacade facade, ITeacherFacade teacherFacade)
    {
        _facade = facade;
        _teacherFacade = teacherFacade;
    }

    public CourseFilterResult FilterResult { get; set; }
    public async Task OnGet()
    {
        var teacher = await _teacherFacade.GetByUserId(User.GetUserId());
        FilterParams.TeacherId = teacher!.Id;
        FilterResult = await _facade.GetByFilter(FilterParams);
    }
}