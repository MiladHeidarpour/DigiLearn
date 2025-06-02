using Common.Query.Filter;
using DigiLearn.Web.Infrastructure.RazorUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketModule.Core.Dtos.Queries;
using TicketModule.Core.Services;

namespace DigiLearn.Web.Areas.Admin.Pages.Tickets;

public class IndexModel : BaseRazorFilter<TicketFilterParams>
{
    private readonly ITicketService _service;

    public IndexModel(ITicketService service)
    {
        _service = service;
    }

    public TicketFilterResult FilterResult { get; set; }

    public async Task OnGet()
    {
        FilterResult = await _service.GetTicketsByFilter(FilterParams);
    }
}