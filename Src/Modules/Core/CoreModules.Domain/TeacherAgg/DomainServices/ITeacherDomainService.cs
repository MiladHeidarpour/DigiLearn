namespace CoreModule.Domain.TeacherAgg.DomainServices;

public interface ITeacherDomainService
{
    bool IsUserNameExist(string userName);
}