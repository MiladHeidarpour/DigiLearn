using Common.Application;
using UserModule.Core.Commands.Users.Register;
using UserModule.Core.Queries.Users.Dtos;

namespace UserModule.Core.Services;

public interface IUserFacade
{
    Task<OperationResult<Guid>> RegisterUser(RegisterUserCommand command);
    Task<UserDto?> GetUserByPhoneNumber(string phoneNumber);
}