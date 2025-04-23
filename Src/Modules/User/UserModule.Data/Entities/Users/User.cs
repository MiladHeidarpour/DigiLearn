using System.ComponentModel.DataAnnotations;
using Common.Domain;

namespace UserModule.Data.Entities.Users;

class User : BaseEntity
{
    [MaxLength(50)]
    public string? Name { get; set; }

    [MaxLength(50)]
    public string? Family { get; set; }

    [Required]
    [MaxLength(11)]
    public string PhoneNumber { get; set; }

    [MaxLength(50)]
    public string? Email { get; set; }

    [Required]
    [MaxLength(70)]
    public string Password { get; set; }

    [Required]
    [MaxLength(100)]
    public string Avatar { get; set; }
    public List<UserRole> UserRoles { get; set; }
}