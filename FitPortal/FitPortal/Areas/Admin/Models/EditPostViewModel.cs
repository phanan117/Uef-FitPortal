using System.ComponentModel.DataAnnotations;

namespace FitPortal.Areas.Admin.Models
{
    public class EditPostViewModel
    {
        public int PostId { get; set; }
        [Required(ErrorMessage = "Vui lòng điền tên bản tin!")]
        public string PostName { get; set; }

        [Required(ErrorMessage = "Vui lòng ghi mô tả cho bản tin!")]
        public string Describe { get; set; }
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Vui lòng điền nội dung bản tin!")]
        public string Content { get; set; }
        public IFormFile? Picture { get; set; }
    }
}
