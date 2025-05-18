using Common.Infrastructure.Repository;
using CoreModules.Domain.TeacherAgg.Models;
using CoreModules.Domain.TeacherAgg.Repositories;

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