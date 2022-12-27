using FitPortal.Models;
using FitPortal.Models.Domain;
using FitPortal.Models.DTO;
using FitPortal.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FitPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DatabaseContext _dbcon;
        private readonly IPostRepository postRepository;
        private readonly ICategoryRepository categoryRepository;
        public HomeController(ILogger<HomeController> logger, DatabaseContext dbcon, ICategoryRepository categoryRepository,IPostRepository postRepository)
        {
            this._logger = logger;
            this._dbcon = dbcon;
            this.categoryRepository = categoryRepository;
            this.postRepository = postRepository;   
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HomeViewModel model = new HomeViewModel();
            try
            {
                var posts = await postRepository.GetAll().Where(p => p.IsDisplay == true).Take(4).ToListAsync();
                var categories = await categoryRepository.GetAll().ToListAsync();
                foreach (var post in posts)
                {
                    EventInformation inforEvent = new EventInformation();
                    inforEvent.Id = post.Id;
                    inforEvent.Title = post.PostName;
                    inforEvent.CreateTime = post.DateCreated;
                    inforEvent.Content = post.Content;
                    inforEvent.Picture = post.Picture;
                    inforEvent.Describe = post.Describe;
                    inforEvent.CategoryId = post.CategoryID;
                    model.AddPost(inforEvent);
                }
                foreach (var category in categories)
                {
                    PostCategory infoCategory = new PostCategory()
                    {
                        CategoryName = category.CategoryName,
                        Id = category.Id
                    };
                    model.AddCategory(infoCategory);
                }
                return View(model);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Login", "UserAuthentication");
            }
            
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}