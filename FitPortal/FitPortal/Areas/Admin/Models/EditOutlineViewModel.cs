using System.ComponentModel.DataAnnotations;

namespace FitPortal.Areas.Admin.Models
{
    public class EditOutlineViewModel
    {
        [Required]
        public string Name { get; set; }
        public int IdOutline { get; set; }
        public IFormFile? File { get; set; }
        public string? FileName { get; set; }
    }
}
