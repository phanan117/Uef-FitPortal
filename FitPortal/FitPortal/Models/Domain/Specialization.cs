using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitPortal.Models.Domain
{
    public class Specialization
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string SpecializationID { get; set; }
        [Required]
        [StringLength(250)]
        public string SpecializationName { get; set; }
        public DateTime DateCreate { get; set; }
        public int? ManagerID { get; set; }
        [ForeignKey("ManagerID")]
        public Teachers Teachers { get; set; }
        public List<TeacherPosition> teacherPositions { get; set; }
        public List<Class> Classes { get; set; }
        public List<SubjectMajors> SubjectMajors { get; set; }
    }
}
