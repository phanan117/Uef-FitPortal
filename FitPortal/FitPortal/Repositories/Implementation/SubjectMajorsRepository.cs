using FitPortal.Models.Domain;
using FitPortal.Repositories.Abstract;

namespace FitPortal.Repositories.Implementation
{
    public class SubjectMajorsRepository : BaseRepository<SubjectMajors>,ISubjectMajorsRepository
    {
        public SubjectMajorsRepository(DatabaseContext ctx) : base(ctx)
        {

        }
    }
}
