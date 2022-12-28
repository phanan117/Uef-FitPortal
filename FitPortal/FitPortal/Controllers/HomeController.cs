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
        private readonly ITeacherRepository teacherRepository;
        public HomeController(ILogger<HomeController> logger, DatabaseContext dbcon, ICategoryRepository categoryRepository,IPostRepository postRepository, ITeacherRepository teacherRepository)
        {
            this._logger = logger;
            this._dbcon = dbcon;
            this.categoryRepository = categoryRepository;
            this.postRepository = postRepository;
            this.teacherRepository = teacherRepository;
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
        [HttpGet]
        public async Task<IActionResult> SearchTeacher() 
        {
            try
            {
                var teachers = await teacherRepository.GetAll().Where(t => t.IsDeleted == false).ToListAsync();
                List<SearchTeacherViewModel> model = new List<SearchTeacherViewModel>();
                foreach(var teacher in teachers)
                {
                    SearchTeacherViewModel itemModel = new SearchTeacherViewModel()
                    {
                        TeacherId = teacher.Id,
                        TeacherName = teacher.Name,
                        PhoneNumber = teacher.PhoneNumber,
                        Email = teacher.Email,
                        picture = teacher.Avatar
                    };
                    model.Add(itemModel);
                }
                var category = await categoryRepository.GetAll().ToListAsync();
                ViewBag.Category = category;
                return View(model);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Index", "Home");
            }    
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}