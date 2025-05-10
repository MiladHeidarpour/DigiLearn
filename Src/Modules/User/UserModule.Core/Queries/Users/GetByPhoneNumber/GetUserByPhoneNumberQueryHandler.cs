using AutoMapper;
using Common.Query;
using Microsoft.EntityFrameworkCore;
using UserModule.Core.Queries.Users.Dtos;
using UserModule.Data;

namespace UserModule.Core.Queries.Users.GetByPhoneNumber;

public class GetUserByPhoneNumberQueryHandler : IQueryHandler<GetUserByPhoneNumberQuery, UserDto?>
{
    private readonly UserContext _context;
    private readonly IMapper _mapper;

    public GetUserByPhoneNumberQueryHandler(UserContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserDto?> Handle(GetUserByPhoneNumberQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(f => f.PhoneNumber == request.PhoneNumber);

        if (user == null)
            return null;

        return _mapper.Map<UserDto>(user);
    }
}