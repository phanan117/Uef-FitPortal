using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitPortal.Models.Domain
{
    public class Teachers
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string TeacherCode { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public DateTime DayOfBirth { get; set; }
        [StringLength(10)]
        public string Gender { get; set; }
        [StringLength(150)]
        public string Address { get; set; }
        [StringLength(20)]
        public string PhoneNumber { get; set; }
        [StringLength(150)]
        public string Email { get; set; }
        [StringLength(500)]
        public string Avatar { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        [StringLength(50)]
        public string Identification { get; set; }
        public bool IsDeleted { get; set; }
        public List<TeacherPosition> teacherPositions { get; set; }
        public List<TeacherUser> teacherUser { get; set; }
    }
}
