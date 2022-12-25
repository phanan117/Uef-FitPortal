using System.ComponentModel.DataAnnotations.Schema;

namespace FitPortal.Models.Domain
{
    public class SubjectMajors
    {
        public int SubjectId { get; set; }
        public int MajorsId { get; set; }
        [ForeignKey("MajorsId")]
        public Specialization Specialization { get; set; }
        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; }
    }
}
