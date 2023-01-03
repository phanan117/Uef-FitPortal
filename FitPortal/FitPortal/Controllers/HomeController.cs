using FitPortal.Models;
using FitPortal.Models.Domain;
using FitPortal.Models.DTO;
using FitPortal.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly ITeacherUserRepository teacherUserRepository;
        private readonly IStudentUserRepository studentUserRepository;
        private readonly IStudentRepository studentepository;
        private readonly UserManager<ApplicationUser> userManager;
        public HomeController(ILogger<HomeController> logger, ITeacherUserRepository teacherUserRepository, IStudentRepository studentepository, IStudentUserRepository studentUserRepository, DatabaseContext dbcon, ICategoryRepository categoryRepository,IPostRepository postRepository, ITeacherRepository teacherRepository, UserManager<ApplicationUser> userManager)
        {
            this._logger = logger;
            this._dbcon = dbcon;
            this.categoryRepository = categoryRepository;
            this.postRepository = postRepository;
            this.teacherRepository = teacherRepository;
            this.userManager = userManager;
            this.studentUserRepository = studentUserRepository;
            this.studentepository = studentepository;
            this.teacherUserRepository = teacherUserRepository;
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
                ViewBag.Category = categories;
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
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> UserProfile()
        {
            try
            {
                var user =await userManager.FindByNameAsync(User.Identity.Name);
                if(user != null)
                {
                    var userInfo =await studentUserRepository.GetAll().Where(su => su.UserID == user.Id).FirstOrDefaultAsync();
                    if(userInfo != null)
                    {
                        var student = studentepository.GetAll().Where(s => s.Id == userInfo.StudentID).FirstOrDefault(); ;
                        if(student != null)
                        {
                            UserProfileViewModel model = new UserProfileViewModel()
                            {
                                UserCode = student.StudentCode,
                                UserName = student.Name,
                                DayOfBirth = student.DayOfBirth,
                                Email = student.Email,
                                PhoneNumber = student.PhoneNumber,
                            };
                            var category = await categoryRepository.GetAll().ToListAsync();
                            ViewBag.Category = category;
                            return View(model);
                        }
                    }
                    else
                    {
                        var teacherInfo =await teacherUserRepository.GetAll().Where(tu => tu.UserID == user.Id).FirstOrDefaultAsync();
                        if(teacherInfo != null)
                        {
                            var teacher = teacherRepository.GetAll().Where(t => t.Id == teacherInfo.TeacherID).FirstOrDefault();
                            if(teacher != null)
                            {
                                UserProfileViewModel model = new UserProfileViewModel()
                                {
                                    UserCode = teacher.TeacherCode,
                                    UserName = teacher.Name,
                                    DayOfBirth = teacher.DayOfBirth,
                                    Email = teacher.Email,
                                    PhoneNumber = teacher.PhoneNumber,
                                };
                                var category = await categoryRepository.GetAll().ToListAsync();
                                ViewBag.Category = category;
                                return View(model);
                            }
                        }
                    }
                    
                }
                return RedirectToAction("Index","Home");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("UserAuthentication", "Login");
            }
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}