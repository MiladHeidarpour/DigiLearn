namespace CoreModules.Domain.TeacherAgg.DomainServices;

public interface ITeacherDomainService
{
    bool IsUserNameExist(string userName);
}