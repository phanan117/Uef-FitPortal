using System.ComponentModel.DataAnnotations;

namespace FitPortal.Areas.Admin.Models
{
    public class EditClassViewModel
    {
        [Required(ErrorMessage = "Không tìm thấy lớp học")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mã lớp")]
        public string ClassCode { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số lượng của lớp")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn giáo viên chủ nhiệm")]
        public int TeacherID { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn chuyên ngành")]
        public int SpecializationID { get; set; }
    }
}
