using Common.Application;
using Common.Application.FileUtil.Interfaces;
using CoreModule.Application._Utilities;
using CoreModule.Domain.TeacherAgg.DomainServices;
using CoreModule.Domain.TeacherAgg.Models;
using CoreModule.Domain.TeacherAgg.Repositories;

namespace CoreModule.Application.TeacherAgg.Register;

public class RegisterTeacherCommandHandler : IBaseCommandHandler<RegisterTeacherCommand>
{
    private readonly ITeacherRepository _repository;
    private readonly ITeacherDomainService _domainService;
    private readonly ILocalFileService _localFileService;

    public RegisterTeacherCommandHandler(ITeacherRepository repository, ITeacherDomainService domainService, ILocalFileService localFileService)
    {
        _repository = repository;
        _domainService = domainService;
        _localFileService = localFileService;
    }

    public async Task<OperationResult> Handle(RegisterTeacherCommand request, CancellationToken cancellationToken)
    {
        var cvFileName = await _localFileService.SaveFileAndGenerateName(request.CvFile, CoreModuleDirectories.CvFileName);
        var teacher = new Teacher(request.UserId, request.UserName, cvFileName, _domainService);
        _repository.Add(teacher);
        await _repository.Save();
        return OperationResult.Success();
    }
}