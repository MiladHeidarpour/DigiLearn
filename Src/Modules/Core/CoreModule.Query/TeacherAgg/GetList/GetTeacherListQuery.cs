using Common.Query;
using CoreModule.Domain.TeacherAgg.Enums;
using CoreModule.Query._Data;
using CoreModule.Query._Dtos;
using CoreModule.Query.TeacherAgg._Dtos;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.TeacherAgg.GetList;

public class GetTeacherListQuery:IQuery<List<TeacherDto>>
{
    
}

class GetTeacherListQueryHandler:IQueryHandler<GetTeacherListQuery,List<TeacherDto>>
{
    private readonly QueryContext _context;

    public GetTeacherListQueryHandler(QueryContext context)
    {
        _context = context;
    }

    public async Task<List<TeacherDto>> Handle(GetTeacherListQuery request, CancellationToken cancellationToken)
    {
        var model = await _context.Teachers
            .Include(f=>f.User)
            .Select(teacher=>new TeacherDto
            {
                Id = teacher.Id,
                CreationDate = teacher.CreationDate,
                UserName = teacher.UserName,
                CvFileName = teacher.CvFileName,
                Status = teacher.Status,
                User = new CoreModuleUserDto
                {
                    Id = teacher.User.Id,
                    CreationDate = teacher.User.CreationDate,
                    Name = teacher.User.Name,
                    Family = teacher.User.Family,
                    PhoneNumber = teacher.User.PhoneNumber,
                    Email = teacher.User.Email,
                    Avatar = teacher.User.Avatar
                }
            }).ToListAsync(cancellationToken);

        return model;
    }
}