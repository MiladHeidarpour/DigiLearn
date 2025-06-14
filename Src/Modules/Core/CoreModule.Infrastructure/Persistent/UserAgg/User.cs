using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Domain;

namespace CoreModule.Infrastructure.Persistent.UserAgg;

public class User : BaseEntity
{
    [MaxLength(12)]
    public string PhoneNumber { get; set; }

    [MaxLength(50)]
    public string? Name { get; set; }

    [MaxLength(50)]
    public string? Family { get; set; }

    [MaxLength(100)]
    public string? Email { get; set; }

    [MaxLength(110)]
    public string? Avatar { get; set; }
}