using Common.Query;

namespace CoreModule.Query._Dtos;

public class CoreModuleUserDto:BaseDto
{
    public string Name { get; set; }
    public string Family { get; set; }
    public string PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string Avatar { get; set; }
}