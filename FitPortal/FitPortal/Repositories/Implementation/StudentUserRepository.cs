using FitPortal.Models.Domain;
using FitPortal.Repositories.Abstract;

namespace FitPortal.Repositories.Implementation
{
    public class StudentUserRepository : BaseRepository<StudentUser>, IStudentUserRepository
    {
        public StudentUserRepository(DatabaseContext ctx) : base(ctx)
        {

        }
    }
}
