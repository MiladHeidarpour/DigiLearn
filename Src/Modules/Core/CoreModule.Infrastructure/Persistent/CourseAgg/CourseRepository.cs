using Common.Infrastructure.Repository;
using CoreModule.Domain.CourseAgg.Models;
using CoreModule.Domain.CourseAgg.Repositories;

namespace CoreModule.Infrastructure.Persistent.CourseAgg;

public class CourseRepository:BaseRepository<Course,CoreModuleEFContext>,ICourseRepository
{
    public CourseRepository(CoreModuleEFContext context) : base(context)
    {
    }
}