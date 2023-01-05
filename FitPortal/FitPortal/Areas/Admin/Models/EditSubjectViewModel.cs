using System.ComponentModel.DataAnnotations;

namespace FitPortal.Areas.Admin.Models
{
    public class EditSubjectViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mã môn học")]
        public string SubjectCode { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên môn học")]
        public string SubjectName { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn chuyên ngành")]
        public int MajorId { get; set; }
    }
}
