using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.Utils;
using CoreModule.Domain.TeacherAgg.DomainServices;
using CoreModule.Domain.TeacherAgg.Enums;
using CoreModule.Domain.TeacherAgg.Events;

namespace CoreModule.Domain.TeacherAgg.Models;

public class Teacher : AggregateRoot
{
    private Teacher()
    {
        
    }
    public Teacher(Guid userId, string userName, string cvFileName, ITeacherDomainService domainService)
    {
        Guard(userName, cvFileName);

        if (domainService.IsUserNameExist(userName))
        {
            throw new InvalidDomainDataException("UserName Is Exist");
        }

        UserId = userId;
        UserName = userName.ToLower();
        CvFileName = cvFileName;
        Status = TeacherStatus.Pending;
    }

    public Guid UserId { get; private set; }
    public string UserName { get; private set; }
    public string CvFileName { get; private set; }
    public TeacherStatus Status { get; private set; }


    public void ToggleStatus()
    {
        if (Status==TeacherStatus.Active)
        {
            Status = TeacherStatus.Inactive;
        }
        else if (Status==TeacherStatus.Inactive)
        {
            Status = TeacherStatus.Active;

        }
    }
    public void AcceptRequest()
    {
        if (Status==TeacherStatus.Pending)
        {
            //Event to send Notification
            AddDomainEvent(new AcceptTeacherRequestEvent()
            {
                UserId = UserId
            });
            Status=TeacherStatus.Active;
        }
    }


    void Guard(string userName, string cvFileName)
    {
        NullOrEmptyDomainDataException.CheckString(userName, nameof(userName));
        NullOrEmptyDomainDataException.CheckString(cvFileName, nameof(cvFileName));
        if (userName.IsUniCode())
        {
            throw new InvalidDomainDataException("UserName Is Invalid");
        }
    }
}
