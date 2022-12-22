using FitPortal.Models.Domain;
using FitPortal.Repositories.Abstract;

namespace FitPortal.Repositories.Implementation
{
    public class TeacherUserRepository : BaseRepository<TeacherUser>, ITeacherUserRepository
    {
        public TeacherUserRepository(DatabaseContext ctx) : base(ctx)
        {

        }
    }
}
