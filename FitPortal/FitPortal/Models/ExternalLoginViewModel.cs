using System.ComponentModel.DataAnnotations;

namespace FitPortal.Models
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string AuthenCode { get; set; }
        [Required]
        public string FullName { get; set; }
    }
}
