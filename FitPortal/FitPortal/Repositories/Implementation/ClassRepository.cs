using FitPortal.Models.Domain;
using FitPortal.Repositories.Abstract;

namespace FitPortal.Repositories.Implementation
{
    public class ClassRepository : BaseRepository<Class> , IClassRepository
    {
        public ClassRepository(DatabaseContext ctx) : base(ctx)
        {

        }
    }
}
