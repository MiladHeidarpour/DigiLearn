using CoreModule.Domain.CourseAgg.Enums;
using CoreModule.Facade.CourseAgg;
using CoreModule.Query.CourseAgg._Dtos;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigiLearn.Web.Pages;

public class SearchModel : BaseRazorFilter<CourseFilterParams>
{
    private readonly ICourseFacade _courseFacade;

    public SearchModel(ICourseFacade courseFacade)
    {
        _courseFacade = courseFacade;
    }

    public CourseFilterResult FilterResult { get; set; }

    public async Task OnGet()
    {
        FilterParams.ActionStatus = CourseActionStatus.Active;
        FilterParams.TeacherId = null;
        FilterResult = await _courseFacade.GetByFilter(FilterParams);
    }
}