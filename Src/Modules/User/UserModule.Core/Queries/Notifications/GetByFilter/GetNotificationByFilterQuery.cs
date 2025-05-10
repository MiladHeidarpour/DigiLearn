using Common.Query;
using Microsoft.EntityFrameworkCore;
using UserModule.Core.Queries.Notifications.Dtos;
using UserModule.Data;

namespace UserModule.Core.Queries.Notifications.GetByFilter;

public class GetNotificationByFilterQuery : QueryFilter<NotificationFilterResult, NotificationFilterParams>
{
    public GetNotificationByFilterQuery(NotificationFilterParams filterParams) : base(filterParams)
    {
    }
}

public class GetNotificationByFilterQueryHandler : IQueryHandler<GetNotificationByFilterQuery, NotificationFilterResult>
{
    private readonly UserContext _context;

    public GetNotificationByFilterQueryHandler(UserContext context)
    {
        _context = context;
    }

    public async Task<NotificationFilterResult> Handle(GetNotificationByFilterQuery request, CancellationToken cancellationToken)
    {
        var result = _context.Notifications.Where(r => r.UserId == request.FilterParams.UserId).AsQueryable();

        if (request.FilterParams.IsSeen is not null)
        {
            if (request.FilterParams.IsSeen.Value is true)
            {
                result = result.Where(r => r.IsSeen == true);
            }
            else
            {
                result = result.Where(r => r.IsSeen == false);
            }
        }

        var skip = (request.FilterParams.PageId - 1) * request.FilterParams.Take;

        var model = new NotificationFilterResult()
        {
            Data = await result.Skip(skip).Take(request.FilterParams.Take)
                .Select(s => new NotificationFilterData
                {
                    Id = s.Id,
                    CreationDate = s.CreationDate,
                    UserId = s.UserId,
                    Title = s.Title,
                    Text = s.Text,
                    IsSeen = s.IsSeen
                }).ToListAsync()
        };
        model.GeneratePaging(result, request.FilterParams.Take, request.FilterParams.PageId);
        return model;
    }
}