using System.ComponentModel.DataAnnotations;

namespace FitPortal.Areas.Admin.Models
{
    public class TeacherViewModel
    {
        public int? ID { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mã giảng viên")]
        public string TeacherCode { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập tên giảng viên")]
        public string Name { get; set; }
        public DateTime DayOfBirth { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên giảng viên")]
        public string Gender { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập đỉa chỉ")]
        public string Address { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập số điện thoại")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập Email")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Vui lòng thêm ảnh chân dung")]
        public IFormFile Avatar { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập số chứng minh nhân dân")]
        public string Identification { get; set; }
    }
}
