using Common.Domain.Repository;
using CoreModules.Domain.CategoryAgg.Models;

namespace CoreModules.Domain.CategoryAgg.Repositories;

public interface ICourseCategoryRepository:IBaseRepository<CourseCategory>
{
    Task Delete(CourseCategory category);
}