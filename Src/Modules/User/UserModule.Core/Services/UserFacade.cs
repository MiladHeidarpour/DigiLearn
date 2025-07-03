using Common.Application;
using MediatR;
using UserModule.Core.Commands.ChangePassword;
using UserModule.Core.Commands.Users.Edit;
using UserModule.Core.Commands.Users.Register;
using UserModule.Core.Queries.Users.Dtos;
using UserModule.Core.Queries.Users.GetByFilter;
using UserModule.Core.Queries.Users.GetById;
using UserModule.Core.Queries.Users.GetByPhoneNumber;

namespace UserModule.Core.Services;

public class UserFacade : IUserFacade
{
    private readonly IMediator _mediator;

    public UserFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult<Guid>> RegisterUser(RegisterUserCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditUserProfile(EditUserCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> ChangePassword(ChangeUserPasswordCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<UserDto?> GetUserByPhoneNumber(string phoneNumber)
    {
        return await _mediator.Send(new GetUserByPhoneNumberQuery(phoneNumber));
    }

    public async Task<UserDto?> GetUserById(Guid id)
    {
        return await _mediator.Send(new GetUserByIdQuery(id));
    }

    public async Task<UserFilterResult> GetUserByFilter(UserFilterParams filterParams)
    {
        return await _mediator.Send(new GetUsersByFilterQuery(filterParams));
    }
}