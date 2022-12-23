using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitPortal.Models.Domain
{
    public class Class
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string ClassCode { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int Current { get; set; }
        [Required]
        public int TeacherID { get; set; }
        public int? SpeID { get; set; }
        [ForeignKey("TeacherID")]
        public Teachers Teachers { get; set; }
        [ForeignKey("SpeID")]
        public Specialization Specialization { get; set; }
        public List<Students> Students { get; set; }
    }
}
