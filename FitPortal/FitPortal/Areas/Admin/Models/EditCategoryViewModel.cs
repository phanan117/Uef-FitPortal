using System.ComponentModel.DataAnnotations;

namespace FitPortal.Areas.Admin.Models
{
    public class EditCategoryViewModel
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập thên danh mục")]
        public string CategoryName { get; set; }
    }
}
