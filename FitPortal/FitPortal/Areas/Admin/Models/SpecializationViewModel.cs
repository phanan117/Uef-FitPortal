using System.ComponentModel.DataAnnotations;

namespace FitPortal.Areas.Admin.Models
{
    public class SpecializationViewModel
    {
        public int? ID  { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập mã chuyên ngành")]
        public string SpecializationID { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên chuyên ngành")]
        public string SpecializationName { get; set; }
        [Required(ErrorMessage ="Vui lòng chọn ngày thành lập")]
        public DateTime DateCreate { get; set; }
        public string? ManagerName { get; set; }
        public int? ManagerID { get; set; }
    }
}
