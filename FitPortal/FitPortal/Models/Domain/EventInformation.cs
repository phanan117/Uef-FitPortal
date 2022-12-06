using System.ComponentModel.DataAnnotations;

namespace FitPortal.Models.Domain
{
    public class EventInformation
    {
        [Key]
        public int Id { get; set; }
        public string PostName { get; set; }
        public string Describe { get; set; }
        public string Content { get; set; }
    }
}
