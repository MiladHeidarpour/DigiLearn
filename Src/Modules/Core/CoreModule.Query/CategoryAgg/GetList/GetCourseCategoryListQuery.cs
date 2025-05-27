using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.CategoryAgg._Dtos;
using CoreModule.Query.CategoryAgg.GetChildren;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.CategoryAgg.GetList;

public record GetCourseCategoryListQuery:IQuery<List<CourseCategoryDto>>
{
    
}

class GetCourseCategoryListQueryHandler:IQueryHandler<GetCourseCategoryListQuery,List<CourseCategoryDto>>
{
    private readonly QueryContext _context;

    public GetCourseCategoryListQueryHandler(QueryContext context)
    {
        _context = context;
    }

    public async Task<List<CourseCategoryDto>> Handle(GetCourseCategoryListQuery request, CancellationToken cancellationToken)
    {
        var mainModel = await _context.CourseCategories
            .Include(c => c.Children)
            .Where(r=>r.ParentId==null)
            .OrderByDescending(d => d.CreationDate)
            .Select(s => new CourseCategoryDto
            {
                Id = s.Id,
                CreationDate = s.CreationDate,
                Title = s.Title,
                Slug = s.Slug,
                ParentId = s.ParentId,
                Children = s.Children.Select(r => new CourseCategoryChild()
                {
                    CreationDate = r.CreationDate,
                    Id = r.Id,
                    ParentId = r.ParentId,
                    Slug = r.Slug,
                    Title = r.Title
                }).ToList()
            }).ToListAsync(cancellationToken);

        return mainModel;
    }
}