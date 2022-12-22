using System.ComponentModel.DataAnnotations;

namespace FitPortal.Models.Domain
{
    public class Works
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(500)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime DateStart { get; set; }
        [Required]
        public DateTime DateEnd { get; set; }
        [Required]
        public bool IsTaked { get; set; }
        [Required]
        public DateTime DateCreate { get; set; }
        [Required]
        public DateTime LastMofify { get; set; }
        [Required]
        public bool Status { get; set; }
        public virtual ICollection<Teachers> Teachers { get; set; }
    }
}
