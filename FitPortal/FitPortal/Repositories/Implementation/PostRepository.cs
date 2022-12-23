using FitPortal.Models.Domain;
using FitPortal.Repositories.Abstract;

namespace FitPortal.Repositories.Implementation
{
    public class PostRepository : BaseRepository<PostInfor>,IPostRepository
    {
        public PostRepository(DatabaseContext ctx) : base(ctx)
        {

        }
    }
}
