﻿using Common.Application;
using UserModule.Core.Commands.Users.ChangeAvatar;
using UserModule.Core.Commands.Users.ChangePassword;
using UserModule.Core.Commands.Users.Edit;
using UserModule.Core.Commands.Users.FullEdit;
using UserModule.Core.Commands.Users.Register;
using UserModule.Core.Queries.Users.Dtos;

namespace UserModule.Core.Services;

public interface IUserFacade
{
    Task<OperationResult<Guid>> RegisterUser(RegisterUserCommand command);
    Task<OperationResult> EditUser(FullEditUserCommand command);
    Task<OperationResult> ChangeAvatar(ChangeUserAvatarCommand command);
    Task<OperationResult> EditUserProfile(EditUserCommand command);
    Task<OperationResult> ChangePassword(ChangeUserPasswordCommand command);
    Task<UserDto?> GetUserByPhoneNumber(string phoneNumber);
    Task<UserDto?> GetUserById(Guid id);
    Task<UserFilterResult> GetUserByFilter(UserFilterParams filterParams);
}