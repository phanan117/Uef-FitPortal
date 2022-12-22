using FitPortal.Models.Domain;
using FitPortal.Repositories.Abstract;

namespace FitPortal.Repositories.Implementation
{
    public class TeacherPositionRepository : BaseRepository<TeacherPosition>, ITeacherPositionRepository
    {
        public TeacherPositionRepository(DatabaseContext ctx):base(ctx)
        {

        }
    }
}
