using Microsoft.AspNetCore.Identity;

namespace FitPortal.Models.Domain
{
    public class ApplicationUser:IdentityUser
    {
        public string? Name { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
