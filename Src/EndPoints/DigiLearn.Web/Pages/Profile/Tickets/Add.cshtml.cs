using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using DigiLearn.Web.Infrastructure;
using TicketModule.Core.Dtos.Commands;
using TicketModule.Core.Services;
using UserModule.Core.Services;

namespace DigiLearn.Web.Pages.Profile.Tickets;

[BindProperties]
public class AddModel : BaseRazor
{
    private readonly IUserFacade _userFacade;
    private readonly ITicketService _ticketService;
    public AddModel(IUserFacade userFacade, ITicketService ticketService)
    {
        _userFacade = userFacade;
        _ticketService = ticketService;
    }

    [Required(ErrorMessage = "{0} را وارد کنید")]
    [Display(Name = "عنوان تیکت")]
    public string Title { get; set; }


    [Required(ErrorMessage = "{0} را وارد کنید")]
    [Display(Name = "متن تیکت")]
    [DataType(DataType.MultilineText)]
    public string Text { get; set; }
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        var user = await _userFacade.GetUserByPhoneNumber(User.GetUserPhoneNumber());

        var command = new CreateTicketCommand
        {
            UserId = user.Id,
            OwnerFullName = $"{user.Name} {user.Family}",
            PhoneNumber = user.PhoneNumber,
            Title = Title,
            Text = Text
        };

        var result = await _ticketService.CreateTicket(command);
        return RedirectAndShowAlert(result, RedirectToPage("Index"));
    }
}