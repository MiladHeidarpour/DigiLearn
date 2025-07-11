using Common.Infrastructure.Repository;
using CoreModule.Domain.HelperEntities;
using CoreModule.Domain.HelperEntities.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Infrastructure.Persistent.HelperEntities.Repositories;

public class CourseStudentRepository:BaseRepository<CourseStudent,CoreModuleEFContext>,ICourseStudentRepository
{
    public CourseStudentRepository(CoreModuleEFContext context) : base(context)
    {
    }

    public async Task<CourseStudent?> GetCourseStudent(Guid courseId, Guid userId)
    {
        return await Context.CourseStudents.FirstOrDefaultAsync(f => f.CourseId == courseId && f.UserId == userId);
    }

    public void Delete(CourseStudent student)
    {
        Context.CourseStudents.Remove(student);
    }
}