using Common.Infrastructure.Repository;
using CoreModules.Domain.CourseAgg.Models;
using CoreModules.Domain.CourseAgg.Repositories;

namespace CoreModule.Infrastructure.Persistent.CourseAgg;

public class CourseRepository:BaseRepository<Course,CoreModuleEFContext>,ICourseRepository
{
    public CourseRepository(CoreModuleEFContext context) : base(context)
    {
    }
}