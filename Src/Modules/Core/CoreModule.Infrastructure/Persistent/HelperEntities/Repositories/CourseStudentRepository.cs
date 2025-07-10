using Common.Infrastructure.Repository;
using CoreModule.Domain.HelperEntities;
using CoreModule.Domain.HelperEntities.Repositories;

namespace CoreModule.Infrastructure.Persistent.HelperEntities.Repositories;

public class CourseStudentRepository:BaseRepository<CourseStudent,CoreModuleEFContext>,ICourseStudentRepository
{
    public CourseStudentRepository(CoreModuleEFContext context) : base(context)
    {
    }
}