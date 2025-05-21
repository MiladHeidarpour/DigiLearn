using Common.Application;
using CoreModule.Application.TeacherAgg.AcceptRequest;
using CoreModule.Application.TeacherAgg.Register;
using CoreModule.Application.TeacherAgg.RejectRequest;
using CoreModule.Query.TeacherAgg._Dtos;
using CoreModule.Query.TeacherAgg.GetById;
using CoreModule.Query.TeacherAgg.GetByUserId;
using CoreModule.Query.TeacherAgg.GetList;
using MediatR;

namespace CoreModule.Facade.TeacherAgg;

public interface ITeacherFacade
{
    #region Commands
    
    Task<OperationResult> Register(RegisterTeacherCommand command);
    Task<OperationResult> AcceptRequest(AcceptTeacherRequestCommand command);
    Task<OperationResult> RejectRequest(RejectTeacherRequestCommand command);

    #endregion


    #region Queries

    Task<TeacherDto?> GetById(Guid id);
    Task<TeacherDto?> GetByUserId(Guid userId);
    Task<List<TeacherDto>> GetLis();

    #endregion

}

public class TeacherFacade : ITeacherFacade
{
    private readonly IMediator _mediator;

    public TeacherFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> Register(RegisterTeacherCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AcceptRequest(AcceptTeacherRequestCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> RejectRequest(RejectTeacherRequestCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<TeacherDto?> GetById(Guid id)
    {
        return await _mediator.Send(new GetTeacherByIdQuery(id));
    }

    public async Task<TeacherDto?> GetByUserId(Guid userId)
    {
        return await _mediator.Send(new GetTeacherByUserIdQuery(userId));
    }

    public async Task<List<TeacherDto>> GetLis()
    {
        return await _mediator.Send(new GetTeacherListQuery());
    }
}