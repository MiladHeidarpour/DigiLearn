using Common.Application;
using Microsoft.AspNetCore.Http;

namespace CoreModule.Application.TeacherAgg.Register;

public class RegisterTeacherCommand : IBaseCommand
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public IFormFile CvFile { get; set; }
}