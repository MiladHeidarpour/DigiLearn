using Common.Infrastructure.Repository;
using CoreModules.Domain.CategoryAgg.Models;
using CoreModules.Domain.CategoryAgg.Repositories;
using MongoDB.Driver.Linq;

namespace CoreModule.Infrastructure.Persistent.CategoryAgg;

public class CourseCategoryRepository : BaseRepository<CourseCategory, CoreModuleEFContext>, ICourseCategoryRepository
{
    public CourseCategoryRepository(CoreModuleEFContext context) : base(context)
    {
    }

    public async Task Delete(CourseCategory category)
    {
        var categoryHasCourse = await Context.Courses.AnyAsync(f => f.CategoryId == category.Id || f.SubCategoryId == category.Id);
        Context.Remove(category);

        if (categoryHasCourse is true)
        {
            throw new Exception("این دسته بندی دارای چندین دوره است");
        }
        //ToDo Should Remove Child 
        Context.Remove(category);
        await Context.SaveChangesAsync();
    }
}