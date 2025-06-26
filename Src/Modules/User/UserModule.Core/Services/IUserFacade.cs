using Common.Application;
using UserModule.Core.Commands.ChangePassword;
using UserModule.Core.Commands.Users.Edit;
using UserModule.Core.Commands.Users.Register;
using UserModule.Core.Queries.Users.Dtos;

namespace UserModule.Core.Services;

public interface IUserFacade
{
    Task<OperationResult<Guid>> RegisterUser(RegisterUserCommand command);
    Task<OperationResult> EditUserProfile(EditUserCommand command);
    Task<OperationResult> ChangePassword(ChangeUserPasswordCommand command);
    Task<UserDto?> GetUserByPhoneNumber(string phoneNumber);
    Task<UserDto?> GetUserById(Guid id);
}