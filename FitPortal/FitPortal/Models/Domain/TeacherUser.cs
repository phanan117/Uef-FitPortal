using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitPortal.Models.Domain
{
    public class TeacherUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserID { get; set; }
        [Required]
        public int TeacherID { get; set; }
        [ForeignKey("UserID")]
        public ApplicationUser FitPortalUser { get; set; }
        [ForeignKey("TeacherID")]
        public Teachers teacher { get; set; }
    }
}
