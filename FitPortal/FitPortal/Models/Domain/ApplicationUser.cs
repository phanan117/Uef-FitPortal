using Microsoft.AspNetCore.Identity;

namespace FitPortal.Models.Domain
{
    public class ApplicationUser:IdentityUser
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? ProfilePicture { get; set; }
        public List<PostInfor> posstInfors { get; set; }
        public List<TeacherUser> teacherUser { get; set; }
        public List<StudentUser> studentUser { get; set; }
    }
}
