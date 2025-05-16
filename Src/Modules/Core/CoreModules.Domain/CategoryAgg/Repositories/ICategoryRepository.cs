using Common.Domain.Repository;
using CoreModules.Domain.CategoryAgg.Models;

namespace CoreModules.Domain.CategoryAgg.Repositories;

public interface ICategoryRepository:IBaseRepository<CourseCategory>
{
    Task Delete(CourseCategory category);
}