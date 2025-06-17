using Common.EventBus.Abstractions;
using Common.EventBus.Events;
using DigiLearn.Web.Infrastructure.Services;
using DigiLearn.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigiLearn.Web.Pages;

public class IndexModel : PageModel
{
    private readonly IHomePageService _homePageService;

    public IndexModel(IHomePageService homePageService)
    {
        _homePageService = homePageService;
    }

    public HomePageViewModel HomePageData { get; set; }
    public async Task OnGet()
    {
        HomePageData = await _homePageService.GetData();
    }
}