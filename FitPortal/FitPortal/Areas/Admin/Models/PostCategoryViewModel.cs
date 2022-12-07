using System.ComponentModel.DataAnnotations;

namespace FitPortal.Areas.Admin.Models
{
    public class PostCategoryViewModel
    {
        [Required(ErrorMessage ="Vui lòng nhập thên danh mục")]
        public string CategoryName { get; set; }
    }
}
