using Common.Application;

namespace CoreModule.Application.TeacherAgg.AcceptRequest;

public record AcceptTeacherRequestCommand(Guid TeacherId):IBaseCommand;