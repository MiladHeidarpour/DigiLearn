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
        return await _context.CourseCategories.Where(f => f.ParentId == null).OrderByDescending(f => f.CreationDate)
            .Select(r => new CourseCategoryDto
            {
                Id = r.Id,
                CreationDate = r.CreationDate,
                Title = r.Title,
                Slug = r.Slug,
                ParentId = r.ParentId
            })
            .ToListAsync(cancellationToken);
    }
}