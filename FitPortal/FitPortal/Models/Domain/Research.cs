namespace FitPortal.Models.Domain
{
    public class Research
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEnglish { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string File { get; set; }
        public List<StudentResearch> StudentResearches { get; set; }
    }
}
