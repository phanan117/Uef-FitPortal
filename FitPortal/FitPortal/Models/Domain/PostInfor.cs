using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitPortal.Models.Domain
{
    public class PostInfor
    {
        [Key]
        public int Id { get; set; }
        [StringLength(200)]
        public string PostName { get; set; }
        [StringLength(500)]
        public string Describe { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        [StringLength(500)]
        public string Picture { get; set; }
        public bool IsDisplay { get; set; }
        [Required]
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public PostCategory PostCategory { get; set; }
        [Required]
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
