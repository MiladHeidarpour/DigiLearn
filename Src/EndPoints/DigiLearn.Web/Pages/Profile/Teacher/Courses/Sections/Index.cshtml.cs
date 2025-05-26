using CoreModule.Facade.CourseAgg;
using CoreModule.Facade.TeacherAgg;
using CoreModule.Query.CourseAgg._Dtos;
using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigiLearn.Web.Pages.Profile.Teacher.Courses.Sections;

[ServiceFilter(typeof(TeacherActionFilter))]
public class IndexModel : BaseRazor
{
    private readonly ICourseFacade _courseFacade;
    private readonly ITeacherFacade _teacherFacade;

    public IndexModel(ICourseFacade courseFacade, ITeacherFacade teacherFacade)
    {
        _courseFacade = courseFacade;
        _teacherFacade = teacherFacade;
    }

    public CourseDto Course { get; set; }

    public async Task<IActionResult> OnGet(Guid courseId)
    {
        var course = await _courseFacade.GetById(courseId);
        var teacher = await _teacherFacade.GetByUserId(User.GetUserId());

        if (course is null || course.TeacherId != teacher!.Id) 
            return RedirectToPage("../Index");

        Course = course;
        return Page();
    }
}