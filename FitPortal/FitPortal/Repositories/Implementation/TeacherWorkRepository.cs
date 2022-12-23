using FitPortal.Models.Domain;
using FitPortal.Repositories.Abstract;

namespace FitPortal.Repositories.Implementation
{
    public class TeacherWorkRepository : BaseRepository<TeachersWorks> , ITeacherWorkRepository
    {
        public TeacherWorkRepository(DatabaseContext ctx) : base(ctx)
        {

        }
    }
}
