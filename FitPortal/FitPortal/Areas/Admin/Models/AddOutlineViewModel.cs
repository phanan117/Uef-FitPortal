using System.ComponentModel.DataAnnotations;

namespace FitPortal.Areas.Admin.Models
{
    public class AddOutlineViewModel
    {
        [Required]
        public int IdSubject { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public IFormFile File { get; set; }
    }
}
