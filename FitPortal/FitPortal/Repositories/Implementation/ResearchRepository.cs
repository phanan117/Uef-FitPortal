using FitPortal.Models.Domain;
using FitPortal.Repositories.Abstract;

namespace FitPortal.Repositories.Implementation
{
    public class ResearchRepository : BaseRepository<Research>,IResearchRepository
    {
        public ResearchRepository(DatabaseContext ctx) : base(ctx)
        {

        }
    }
}
