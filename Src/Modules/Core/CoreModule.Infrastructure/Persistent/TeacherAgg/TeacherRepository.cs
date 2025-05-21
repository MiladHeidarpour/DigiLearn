using Common.Infrastructure.Repository;
using CoreModule.Domain.TeacherAgg.Models;
using CoreModule.Domain.TeacherAgg.Repositories;

namespace CoreModule.Infrastructure.Persistent.TeacherAgg;

class TeacherRepository : BaseRepository<Teacher,CoreModuleEFContext>,ITeacherRepository
{
    public TeacherRepository(CoreModuleEFContext context) : base(context)
    {
    }

    public void Delete(Teacher teacher)
    {
        Context.Remove(teacher);
    }
}