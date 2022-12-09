namespace FitPortal.Areas.Admin.Models
{
    public class PostInformationViewModel
    {
        public int Id { get; set; }
        public string PostName { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDisplay { get; set; }
        public string CategoryName { get; set; }
        public string UserCreate { get; set; }
    }
}
