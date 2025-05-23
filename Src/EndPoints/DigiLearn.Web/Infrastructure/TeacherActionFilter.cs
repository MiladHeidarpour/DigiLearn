using CoreModule.Domain.TeacherAgg.Enums;
using CoreModule.Facade.TeacherAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DigiLearn.Web.Infrastructure;

public class TeacherActionFilter:ActionFilterAttribute
{
    private readonly ITeacherFacade _teacherFacade;

    public TeacherActionFilter(ITeacherFacade teacherFacade)
    {
        _teacherFacade = teacherFacade;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.HttpContext.User.Identity.IsAuthenticated is false)
        {
            context.Result = new RedirectResult("/");
        }

        var teacher = await _teacherFacade.GetByUserId(context.HttpContext.User.GetUserId());
        if (teacher is null || teacher.Status is not TeacherStatus.Active)
        {
            context.Result = new RedirectResult("/profile");
        }

        await next();
    }
}