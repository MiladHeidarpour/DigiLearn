using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.CourseAgg._Dtos;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.CourseAgg.GetByFilter;

public class GetCoursesByFilterQuery : QueryFilter<CourseFilterResult, CourseFilterParams>
{
    public GetCoursesByFilterQuery(CourseFilterParams filterParams) : base(filterParams)
    {
    }
}

class GetCoursesByFilterQueryHandler : IQueryHandler<GetCoursesByFilterQuery, CourseFilterResult>
{
    private readonly QueryContext _context;

    public GetCoursesByFilterQueryHandler(QueryContext context)
    {
        _context = context;
    }

    public async Task<CourseFilterResult> Handle(GetCoursesByFilterQuery request, CancellationToken cancellationToken)
    {
        var result = _context.Courses
            .Include(f => f.Sections)
            .ThenInclude(f => f.Episodes)
            .OrderByDescending(f => f.LastUpdate).AsQueryable();
        if (request.FilterParams.TeacherId is not null)
        {
            result = result.Where(f => f.TeacherId == request.FilterParams.TeacherId);
        }

        if (request.FilterParams.ActionStatus is not null)
        {
            result = result.Where(f => f.Status == request.FilterParams.ActionStatus);
        }

        var skip = (request.FilterParams.PageId - 1) * request.FilterParams.Take;
        var data = await result.Skip(skip).Take(request.FilterParams.Take).ToListAsync(cancellationToken);
        var model = new CourseFilterResult()
        {
            Data = data.Select(s => new CourseFilterData
            {
                Id = s.Id,
                CreationDate = s.CreationDate,
                Title = s.Title,
                ImageName = s.ImageName,
                Slug = s.Slug,
                Price = s.Price,
                EpisodeCount = s.Sections.Sum(b => b.Episodes.Count()),
                CourseActionStatus = s.Status
            }).ToList()
        };
        model.GeneratePaging(result, request.FilterParams.Take, request.FilterParams.PageId);
        return model;
    }
}