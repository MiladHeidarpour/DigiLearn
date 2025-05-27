using Common.Infrastructure.Repository;
using CoreModule.Domain.CategoryAgg.Models;
using CoreModule.Domain.CategoryAgg.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Infrastructure.Persistent.CategoryAgg;

public class CourseCategoryRepository : BaseRepository<CourseCategory, CoreModuleEFContext>, ICourseCategoryRepository
{
    public CourseCategoryRepository(CoreModuleEFContext context) : base(context)
    {
    }

    public async Task Delete(CourseCategory category)
    {
        var categoryHasCourse = await Context.Courses.AnyAsync(f => f.CategoryId == category.Id || f.SubCategoryId == category.Id);

        if (categoryHasCourse is true)
        {
            throw new Exception("این دسته بندی دارای چندین دوره است");
        }

        var children = await Context.Categories.Where(r => r.ParentId == category.Id).ToListAsync();

        if (children.Any())
        {
            foreach (var item in children)
            {
                var isAnyCourse = await Context.Courses.AnyAsync(f => f.CategoryId == category.Id || f.SubCategoryId == category.Id);

                if (isAnyCourse is true)
                {
                    throw new Exception("این دسته بندی دارای چندین دوره است");
                }
                else
                {
                    Context.Remove(item);
                }
            }
        }
        
        Context.Remove(category);
        await Context.SaveChangesAsync();
    }
}