using FitPortal.Models.DTO;
using FitPortal.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitPortal.Controllers
{
    public class ViewPostController : Controller
    {
        private readonly IPostRepository postRepository;
        private readonly ICategoryRepository categoryRepository;
        public ViewPostController(IPostRepository postRepository,ICategoryRepository categoryRepository)
        {
            this.postRepository = postRepository;
            this.categoryRepository = categoryRepository;
        }
        [HttpGet]
        public async Task<IActionResult> View(int IDPost)
        {
            try
            {
                var category = await categoryRepository.GetAll().ToListAsync();
                var post = postRepository.GetAll().Where(p => p.Id == IDPost).FirstOrDefault();
                ViewBag.Category = category;
                return View(post);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Home", "Index");
            }
            
        }
        [HttpGet]
        public async Task<IActionResult> Category(int IDCategory)
        {
            try
            {
                var category = await categoryRepository.GetAll().ToListAsync();
                ViewBag.Category = category;
                ViewBag.CateName = (from c in category
                                    where c.Id == IDCategory
                                    select c.CategoryName).FirstOrDefault();
                var posts = await postRepository.GetAll().Where(p => p.CategoryID == IDCategory).ToListAsync();
                List<EventInformation> model = new List<EventInformation>();
                foreach(var post in posts)
                {
                    EventInformation itemModel = new EventInformation()
                    {
                        Id = post.Id,
                        Title = post.PostName,
                        CreateTime = post.DateCreated,
                        Content = post.Content,
                        Picture = post.Picture,
                        Describe = post.Describe,
                        CategoryId = post.CategoryID
                    };
                    model.Add(itemModel);
                }
                return View(model);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Home", "Index");
            }
        }
    }
}
