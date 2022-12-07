using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitPortal.Models.Domain
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<PostInfor> PostInformation { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options):base(options)
        {

        }

    }
}
