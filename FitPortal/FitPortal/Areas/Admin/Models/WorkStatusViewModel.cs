namespace FitPortal.Areas.Admin.Models
{
    public class WorkStatusViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public bool IsTaked { get; set; }
    }
}
