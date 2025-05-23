﻿using Common.Application;
using MediatR;
using UserModule.Core.Commands.ChangePassword;
using UserModule.Core.Commands.Users.Edit;
using UserModule.Core.Commands.Users.Register;
using UserModule.Core.Queries.Users.Dtos;
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
}