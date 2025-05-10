using Common.Query;
using UserModule.Core.Queries.Users.Dtos;

namespace UserModule.Core.Queries.Users.GetByPhoneNumber;

public record GetUserByPhoneNumberQuery(string PhoneNumber) : IQuery<UserDto?>;