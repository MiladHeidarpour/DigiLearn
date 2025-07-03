using System.ComponentModel.DataAnnotations;
using UserModule.Data.Entities._Enums;

namespace DigiLearn.Web.ViewModels.Admins;

public class EditRoleViewModel
{
    public Guid RoleId { get; set; }
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Name { get; set; }
    public List<Permission> SelectedPermissions { get; set; } = new();
}