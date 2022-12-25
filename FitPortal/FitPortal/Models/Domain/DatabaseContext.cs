using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitPortal.Models.Domain
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Many to many subject and mojors
            //Tạo quan hệ nhiều nhiều tron entity framework
            builder.Entity<SubjectMajors>() //Set khóa chính cho bảng quan hệ là hai cột
                   .HasKey(sm => new { sm.SubjectId, sm.MajorsId });
            builder.Entity<SubjectMajors>() //thiết lập phần nhiều cho bảng môn học
                   .HasOne(sm => sm.Subject)
                   .WithMany(sm => sm.SubjectMajors)
                   .HasForeignKey(sm => sm.SubjectId);
            builder.Entity<SubjectMajors>() //Thiết lập phần nhiều cho bảng chuyên ngành
                   .HasOne(sm => sm.Specialization)
                   .WithMany(sm => sm.SubjectMajors)
                   .HasForeignKey(sm => sm.MajorsId);
        }
        public DbSet<PostInfor> Posts { get; set; }
        public DbSet<PostCategory> Categories { get; set; }
        public DbSet<TeacherPosition> TeacherPositions { get; set; }
        public DbSet<Teachers> Teachers { get; set; }
        public DbSet<TeacherUser> TeacherUser { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<TeachersWorks> TeachersWorks { get; set; }
        public DbSet<StudentUser> StudentUsers { get; set; }
        public DbSet<Students> Students { get; set; }
        public DbSet<Class> Class { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectMajors> SubjectMajors { get; set; }
    }
}
