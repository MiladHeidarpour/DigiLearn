using CoreModule.Facade.CourseAgg;
using CoreModule.Query.CourseAgg._Dtos;
using DigiLearn.Web.Infrastructure.RazorUtils;

namespace DigiLearn.Web.Areas.Admin.Pages.Courses;

public class IndexModel : BaseRazorFilter<CourseFilterParams>
{
    private ICourseFacade _courseFacade;

    public IndexModel(ICourseFacade courseFacade)
    {
        _courseFacade = courseFacade;
    }

    public CourseFilterResult FilterResult { get; set; }
    public async Task OnGet()
    {
        FilterResult = await _courseFacade.GetByFilter(FilterParams);
    }
}