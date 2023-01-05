using FitPortal.Models.Domain;
using FitPortal.Repositories.Abstract;

namespace FitPortal.Repositories.Implementation
{
    public class OutlineRepository : BaseRepository<Outline>,IOutlineRepository
    {
        public OutlineRepository(DatabaseContext ctx) : base(ctx)
        {

        }
    }
}
