using Common.Application;

namespace CoreModule.Application.TeacherAgg.RejectRequest;

public record RejectTeacherRequestCommand(Guid TeacherId,string Description):IBaseCommand;