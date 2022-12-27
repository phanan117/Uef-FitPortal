using FitPortal.Models.Domain;
using FitPortal.Repositories.Abstract;

namespace FitPortal.Repositories.Implementation
{
    public class CategoryRepository : BaseRepository<PostCategory>,ICategoryRepository
    {
        public CategoryRepository(DatabaseContext ctx) : base(ctx)
        {

        }
    }
}
