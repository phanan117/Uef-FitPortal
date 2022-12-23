using FitPortal.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace FitPortal.Controllers
{
    public class ViewPostController : Controller
    {
        private readonly IPostRepository postRepository;
        public ViewPostController(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }
        public IActionResult View(int IDPost)
        {
            var post = postRepository.GetAll().Where(p => p.Id == IDPost).FirstOrDefault();
            return View(post);
        }
    }
}
