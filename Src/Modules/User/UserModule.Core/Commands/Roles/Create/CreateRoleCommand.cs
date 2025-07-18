﻿using Common.Application;
using Microsoft.EntityFrameworkCore;
using UserModule.Data;
using UserModule.Data.Entities._Enums;
using UserModule.Data.Entities.Roles;

namespace UserModule.Core.Commands.Roles.Create;

public class CreateRoleCommand : IBaseCommand
{
    public string Name { get; set; }
    public List<Permission> Permissions { get; set; } = new();
}

class CreateRoleCommandHandler : IBaseCommandHandler<CreateRoleCommand>
{
    private readonly UserContext _context;

    public CreateRoleCommandHandler(UserContext context)
    {
        _context = context;
    }

    public async Task<OperationResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        if (request.Permissions.Count == 0)
        {
            return OperationResult.Error("لطفا دسترسی ها را مشخص کنید");
        }

        var roleIsExist = await _context.Roles.AnyAsync(f => f.Name == request.Name, cancellationToken);
        if (roleIsExist)
        {
            return OperationResult.Error("این نقش قبلا ساخته شده است");
        }

        var role = new Role()
        {
            Name = request.Name,
        };
        _context.Roles.Add(role);
        foreach (var permission in request.Permissions)
        {
            _context.RolePermissions.Add(new RolePermission()
            {
                RoleId = role.Id,
                Permission = permission
            });
        }
        await _context.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }
}