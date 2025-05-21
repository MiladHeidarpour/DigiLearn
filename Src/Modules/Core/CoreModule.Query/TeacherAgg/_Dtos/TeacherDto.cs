using Common.Query;
using CoreModule.Domain.TeacherAgg.Enums;
using CoreModule.Query._Dtos;

namespace CoreModule.Query.TeacherAgg._Dtos;

public class TeacherDto:BaseDto
{
    public string UserName { get; set; }
    public string CvFileName { get; set; }
    public TeacherStatus Status { get; set; }
    public CoreModuleUserDto User { get; set; }
}