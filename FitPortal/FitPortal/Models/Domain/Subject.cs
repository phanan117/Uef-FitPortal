using System.ComponentModel.DataAnnotations;

namespace FitPortal.Models.Domain
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string SubjectCode { get; set; }
        [Required]
        [StringLength(250)]
        public string SubjectName { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime LastModify { get; set; }
        public List<SubjectMajors> SubjectMajors { get; set; }
        public List<Outline> Outlines { get; set; }
    }
}
