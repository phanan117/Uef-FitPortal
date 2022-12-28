using FitPortal.Models.Domain;
using FitPortal.Repositories.Abstract;

namespace FitPortal.Repositories.Implementation
{
    public class StudentResearchRepository : BaseRepository<StudentResearch>, IStudentResearchRepository
    {
        public StudentResearchRepository(DatabaseContext ctx):base(ctx)
        {

        }
    }
}
