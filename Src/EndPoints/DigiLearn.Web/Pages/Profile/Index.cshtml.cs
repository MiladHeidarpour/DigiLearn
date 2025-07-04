using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserModule.Core.Commands.Users.ChangeAvatar;
using UserModule.Core.Queries.Notifications.Dtos;
using UserModule.Core.Queries.Users.Dtos;
using UserModule.Core.Services;

namespace DigiLearn.Web.Pages.Profile;

public class IndexModel : BaseRazor
{
    private readonly IUserFacade _userFacade;
    private readonly INotificationFacade _notificationFacade;

    public IndexModel(IUserFacade userFacade, INotificationFacade notificationFacade)
    {
        _userFacade = userFacade;
        _notificationFacade = notificationFacade;
    }

    public List<NotificationFilterData> NewNotification { get; set; }
    public UserDto UserDto { get; set; }
    public async Task OnGet()
    {
        UserDto = await _userFacade.GetUserByPhoneNumber(User.GetUserPhoneNumber());
        var result = await _notificationFacade.GetNotificationByFilter(new NotificationFilterParams()
        {
            UserId = UserDto!.Id,
            Take = 5,
            PageId = 1,
            IsSeen = false,
        });
        NewNotification = result.Data;
    }

    public async Task<IActionResult> OnPost(IFormFile avatar)
    {
        var res = await _userFacade.ChangeAvatar(new ChangeUserAvatarCommand()
        {
            UserId = User.GetUserId(),
            AvatarFile = avatar
        });

        return RedirectAndShowAlert(res, Redirect("/profile"));
    }
}