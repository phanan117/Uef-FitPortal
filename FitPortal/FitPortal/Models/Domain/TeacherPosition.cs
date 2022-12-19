using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitPortal.Models.Domain
{
    public class TeacherPosition
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TeacherID { get; set; }
        [Required]
        public int SpecializationID { get; set; }
        [Required]
        [StringLength(150)]
        public string Position { get; set; }
        [ForeignKey("TeacherID")]
        public Teachers Teachers { get; set; }
        [ForeignKey("SpecializationID")]
        public Specialization Specialization { get; set; }
    }
}
