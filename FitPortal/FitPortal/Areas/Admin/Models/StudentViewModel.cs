namespace FitPortal.Areas.Admin.Models
{
    public class StudentViewModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentCode { get; set; }
        public DateTime DayOfBirth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set;}
        public string Email { get; set; }
        public string ClassName { get; set; }
    }
}
