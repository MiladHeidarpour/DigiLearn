using Common.Query;
using CoreModule.Domain.TeacherAgg.Enums;
using CoreModule.Query._Data;
using CoreModule.Query._Dtos;
using CoreModule.Query.TeacherAgg._Dtos;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.TeacherAgg.GetById;

public record GetTeacherByIdQuery(Guid Id) : IQuery<TeacherDto?>;

class GetTeacherByIdQueryHandler : IQueryHandler<GetTeacherByIdQuery, TeacherDto?>
{
    private readonly QueryContext _context;

    public GetTeacherByIdQueryHandler(QueryContext context)
    {
        _context = context;
    }

    public async Task<TeacherDto?> Handle(GetTeacherByIdQuery request, CancellationToken cancellationToken)
    {
        var model = await _context.Teachers
            .Include(f => f.User)
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

        if (model is null)
        {
            return null;
        }

        return new TeacherDto
        {
            Id = model.Id,
            CreationDate = model.CreationDate,
            UserName = model.UserName,
            CvFileName = model.CvFileName,
            Status = model.Status,
            User = new CoreModuleUserDto
            {
                Id = model.User.Id,
                CreationDate = model.User.CreationDate,
                Name = model.User.Name,
                Family = model.User.Family,
                PhoneNumber = model.User.PhoneNumber,
                Email = model.User.Email,
                Avatar = model.User.Avatar
            }
        };
    }
}