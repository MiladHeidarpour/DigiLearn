using Common.Domain.Repository;
using CoreModule.Domain.CategoryAgg.Models;

namespace CoreModule.Domain.CategoryAgg.Repositories;

public interface ICourseCategoryRepository:IBaseRepository<CourseCategory>
{
    Task Delete(CourseCategory category);
}