using System.ComponentModel.DataAnnotations.Schema;

namespace FitPortal.Models.Domain
{
    public class StudentResearch
    {
        public int StudentId { get; set; }
        public int ResearchId { get; set; }
        [ForeignKey("StudentId")]
        public Students Students { get; set; }
        [ForeignKey("ResearchId")]
        public Research Research { get; set; }
    }
}
