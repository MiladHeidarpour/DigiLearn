using DigiLearn.Web.Infrastructure.RazorUtils;
using DigiLearn.Web.Infrastructure.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserModule.Core.Queries.Users.Dtos;
using UserModule.Core.Services;

namespace DigiLearn.Web.Areas.Admin.Pages.Users;

public class IndexModel : BaseRazorFilter<UserFilterParams>
{
    private readonly IUserFacade _userFacade;

    public IndexModel(IUserFacade userFacade)
    {
        _userFacade = userFacade;
    }

    public UserFilterResult FilterResult { get; set; }
    public async Task OnGet(string stDate, string enDate)
    {
        if (string.IsNullOrWhiteSpace(stDate) == false)
        {
            FilterParams.StartDate = stDate.ToMiladi();
        }
        if (string.IsNullOrWhiteSpace(enDate) == false)
        {
            FilterParams.EndDate = enDate.ToMiladi();
        }
        FilterResult = await _userFacade.GetUserByFilter(FilterParams);
    }
}