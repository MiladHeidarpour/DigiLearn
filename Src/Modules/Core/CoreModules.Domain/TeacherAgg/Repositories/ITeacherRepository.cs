using Common.Domain.Repository;
using CoreModule.Domain.TeacherAgg.Models;

namespace CoreModule.Domain.TeacherAgg.Repositories;

public interface ITeacherRepository : IBaseRepository<Teacher>
{
    void Delete(Teacher teacher);
}