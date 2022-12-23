using System.ComponentModel.DataAnnotations;

namespace FitPortal.Areas.Admin.Models
{
    public class AddStudentViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên sinh viên")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mã sinh viên")]
        public string StudentCode { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn ngày sinh")]
        public DateTime DayOfBirth { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn giới tính")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập email")]
        public string Email { get; set; }
    }
}
