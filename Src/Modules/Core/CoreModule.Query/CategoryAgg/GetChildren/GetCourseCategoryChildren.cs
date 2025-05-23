using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.CategoryAgg._Dtos;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.CategoryAgg.GetChildren;

public record GetCourseCategoryChildren(Guid ParentId) : IQuery<List<CourseCategoryDto>>;

class GetCourseCategoryChildrenHandler : IQueryHandler<GetCourseCategoryChildren, List<CourseCategoryDto>>
{
    private readonly QueryContext _context;

    public GetCourseCategoryChildrenHandler(QueryContext context)
    {
        _context = context;
    }

    public async Task<List<CourseCategoryDto>> Handle(GetCourseCategoryChildren request, CancellationToken cancellationToken)
    {
        return await _context.CourseCategories.Where(f => f.ParentId == request.ParentId).OrderByDescending(f => f.CreationDate)
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