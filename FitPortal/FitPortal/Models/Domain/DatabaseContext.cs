using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitPortal.Models.Domain
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<PostInfor> Posts { get; set; }
        public DbSet<PostCategory> Categories { get; set; }
        public DbSet<TeacherPosition> TeacherPositions { get; set; }
        public DbSet<Teachers> Teachers { get; set; }
        public DbSet<TeacherUser> TeacherUser { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options):base(options)
        {

        }

    }
}
