using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitPortal.Models.Domain
{
    public class Students
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string StudentCode { get; set; }
        [Required]
        public DateTime DayOfBirth { get; set; }
        [Required]
        [StringLength(10)]
        public string Gender { get; set; }
        [Required]
        [StringLength(200)]
        public string Address { get; set; }
        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public DateTime LastModify { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        public int? ClassID { get; set; }
        [ForeignKey("ClassID")]
        public Class? Class { get; set; }
        public List<StudentResearch> StudentResearches { get; set; }
    }
}
