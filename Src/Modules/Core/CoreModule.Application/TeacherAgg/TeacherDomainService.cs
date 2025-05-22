using CoreModule.Domain.TeacherAgg.DomainServices;
using CoreModule.Domain.TeacherAgg.Repositories;

namespace CoreModule.Application.TeacherAgg;

public class TeacherDomainService : ITeacherDomainService
{
    private readonly ITeacherRepository _repository;

    public TeacherDomainService(ITeacherRepository repository)
    {
        _repository = repository;
    }

    public bool IsUserNameExist(string userName)
    {
        return _repository.Exists(f => f.UserName == userName.ToLower());
    }
}