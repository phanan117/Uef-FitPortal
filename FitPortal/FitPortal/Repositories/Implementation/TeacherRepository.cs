using FitPortal.Models.Domain;
using FitPortal.Repositories.Abstract;

namespace FitPortal.Repositories.Implementation
{
    public class TeacherRepository : BaseRepository<Teachers>, ITeacherRepository 
    {
        public TeacherRepository(DatabaseContext ctx) : base(ctx)
        {

        }
    }
}
