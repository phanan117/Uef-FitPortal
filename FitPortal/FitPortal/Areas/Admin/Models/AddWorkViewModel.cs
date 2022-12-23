using System.ComponentModel.DataAnnotations;

namespace FitPortal.Areas.Admin.Models
{
    public class AddWorkViewModel
    {
        [Required(ErrorMessage ="Vui lòng nhập tên công việc")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vui ghi rõ mô tả công việc cần phải làm")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ công việc")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn ngày bắt đầu")]
        public DateTime DateStart { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn ngày kết thúc")]
        public DateTime DateEnd { get; set; }
    }
}
