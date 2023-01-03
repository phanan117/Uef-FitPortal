using System.ComponentModel.DataAnnotations;

namespace FitPortal.Areas.Admin.Models
{
    public class PostViewModel
    {
        [Required(ErrorMessage = "Vui lòng điền tên bản tin!")]
        public string PostName { get; set; }

        [Required(ErrorMessage = "Vui lòng ghi mô tả cho bản tin!")]
        public string Describe { get; set; }
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Vui lòng điền nội dung bản tin!")]
        public string Content { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn ngày tạo!")]
        public DateTime DateCreated { get; set; }

        [Required(ErrorMessage = "Vui lòng thêm hình ảnh người dùng!")]
        public IFormFile Picture { get; set; }
    }
}
