using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitPortal.Models.Domain
{
    public class TeacherPosition
    {
        [Key]
        public int Id { get; set; }
        public int? TeacherID { get; set; }
        public int? SpecializationID { get; set; }
        [ForeignKey("TeacherID")]
        public Teachers Teachers { get; set; }
        [ForeignKey("SpecializationID")]
        public Specialization Specialization { get; set; }
    }
}