using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitPortal.Models.Domain
{
    public class TeachersWorks
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TeachersId { get; set; }
        [Required]
        public int WorksId { get; set; }
        [ForeignKey("TeachersId")]
        public Teachers Teachers { get; set; }
        [ForeignKey("WorksId")]
        public Works Works { get; set; }
    }
}
