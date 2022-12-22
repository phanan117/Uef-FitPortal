using FitPortal.Models.Domain;
using FitPortal.Repositories.Abstract;

namespace FitPortal.Repositories.Implementation
{
    public class WorkRepository : BaseRepository<Works>, IWorkRepository
    {
        public WorkRepository(DatabaseContext ctx) : base(ctx)
        {

        }
    }
}
