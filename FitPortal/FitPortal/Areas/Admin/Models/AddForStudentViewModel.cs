using System.ComponentModel.DataAnnotations;

namespace FitPortal.Areas.Admin.Models
{
    public class AddForStudentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Vui lòng thêm file")]
        public IFormFile formFile { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên đề tài")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên tiếng anh cho đề tài")]
        public string NameEnglish { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn ngày bắt đầu")]
        public DateTime DateStart { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn ngày kết thúc")]
        public DateTime DateEnd { get; set; }
    }
}
