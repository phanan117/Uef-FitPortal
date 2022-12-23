using FitPortal.Models.Domain;
using FitPortal.Repositories.Abstract;

namespace FitPortal.Repositories.Implementation
{
    public class StudentRepository : BaseRepository<Students>, IStudentRepository
    {
        public StudentRepository(DatabaseContext ctx) : base(ctx)
        {

        }
    }
}
