using System.ComponentModel.DataAnnotations;

namespace FitPortal.Models.Domain
{
    public class PostCategory
    {
        [Key]
        public int Id { get; set; }
        [StringLength(150)]
        public string CategoryName { get; set; }
        public List<PostInfor> posstInfors { get; set; }
    }
}
