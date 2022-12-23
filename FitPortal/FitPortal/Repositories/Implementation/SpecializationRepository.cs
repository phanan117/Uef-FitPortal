using FitPortal.Models.Domain;
using FitPortal.Repositories.Abstract;

namespace FitPortal.Repositories.Implementation
{
    public class SpecializationRepository : BaseRepository<Specialization>, ISpecializationRepository
    {
        public SpecializationRepository(DatabaseContext ctx):base(ctx)
        {

        }
    }
}
