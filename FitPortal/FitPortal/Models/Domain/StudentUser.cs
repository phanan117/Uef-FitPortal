using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitPortal.Models.Domain
{
    public class StudentUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserID { get; set; }
        [Required]
        public int StudentID { get; set; }
        [ForeignKey("StudentID")]
        public Students Students { get; set; }
        [ForeignKey("UserID")]
        public ApplicationUser User { get; set; }
    }
}
