using Common.Domain;
using UserModule.Data.Entities._Enums;

namespace UserModule.Data.Entities.Roles;

class RolePermission:BaseEntity
{
    public Guid RoleId { get; set; }
    public Permission Permission { get; set; }
    public Role Role { get; set; }
}