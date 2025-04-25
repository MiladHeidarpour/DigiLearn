using DigiLearn.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserModule.Core.Queries.Users.Dtos;
using UserModule.Core.Services;

namespace DigiLearn.Web.Pages.Profile;

public class IndexModel : PageModel
{
    private readonly IUserFacade _userFacade;

    public IndexModel(IUserFacade userFacade)
    {
        _userFacade = userFacade;
    }

    public UserDto UserDto { get; set; }
    public async Task OnGet()
    {
        UserDto = await _userFacade.GetUserByPhoneNumber(User.GetUserPhoneNumber());
    }
}