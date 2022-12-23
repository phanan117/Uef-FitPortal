using System.ComponentModel.DataAnnotations;

namespace FitPortal.Areas.Admin.Models
{
    public class RoleViewModel
    {
        public string? Id { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập tên vai trò")]
        public string RoleName { get; set; }
    }
}
