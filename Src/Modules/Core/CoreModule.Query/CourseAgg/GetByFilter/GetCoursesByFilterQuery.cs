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
            .Include(d=>d.Category)
            .Include(d=>d.SubCategory)
            .Include(c => c.Teacher.User)
            .Include(f => f.Sections)
            .ThenInclude(f => f.Episodes)
            .AsQueryable();

        switch (request.FilterParams.FilterSort)
        {
            case CourseFilterSort.Expensive:
                result = result.OrderByDescending(f => f.LastUpdate);
                break;

            case CourseFilterSort.Oldest:
                result = result.OrderByDescending(f => f.Price);
                break;

            case CourseFilterSort.Latest:
                result = result.OrderBy(f => f.LastUpdate);
                break;
        }

        switch (request.FilterParams.SearchByPrice)
        {
            case SearchByPrice.Free:
                result = result.Where(f => f.Price == 0);
                break;

            case SearchByPrice.NotFree:
                result = result.Where(f => f.Price > 0);
                break;
        }

        if (request.FilterParams.CourseStatus != null)
        {
            result = result.Where(r => r.CourseStatus == request.FilterParams.CourseStatus);
        }

        if (request.FilterParams.CourseLevel != null)
        {
            result = result.Where(r => r.CourseLevel == request.FilterParams.CourseLevel);
        }

        if (request.FilterParams.TeacherId is not null)
        {
            result = result.Where(f => f.TeacherId == request.FilterParams.TeacherId);
        }

        if (request.FilterParams.ActionStatus is not null)
        {
            result = result.Where(f => f.Status == request.FilterParams.ActionStatus);
        }

        if (string.IsNullOrWhiteSpace(request.FilterParams.CategorySlug) == false)
        {
            result = result.Where(r => r.Category.Slug == request.FilterParams.CategorySlug ||
                                       r.SubCategory.Slug == request.FilterParams.CategorySlug);
        }

        if (string.IsNullOrWhiteSpace(request.FilterParams.Search) == false)
        {
            result = result.Where(r => r.Slug.Contains(request.FilterParams.Search) || r.Title.Contains(request.FilterParams.Search));
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
                //EpisodeCount = s.Sections.Sum(b => b.Episodes.Count()),
                CourseActionStatus = s.Status,
                TeacherName = $"{s.Teacher.User.Name} {s.Teacher.User.Family}",
                Sections = s.Sections.Select(f => new CourseSectionDto
                {
                    Id = f.Id,
                    CreationDate = f.CreationDate,
                    CourseId = f.CourseId,
                    Title = f.Title,
                    DisplayOrder = f.DisplayOrder,
                    Episodes = f.Episodes.Select(e => new EpisodeDto
                    {
                        Id = e.Id,
                        CreationDate = e.CreationDate,
                        SectionId = e.SectionId,
                        Title = e.Title,
                        EnglishTitle = e.EnglishTitle,
                        Token = e.Token,
                        TimeSpan = e.TimeSpan,
                        VideoName = e.VideoName,
                        AttachmentName = e.AttachmentName,
                        IsActive = e.IsActive,
                        IsFree = e.IsFree,
                    }).ToList()
                }).ToList(),
            }).ToList()
        };
        model.GeneratePaging(result, request.FilterParams.Take, request.FilterParams.PageId);
        return model;
    }
}