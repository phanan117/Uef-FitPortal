using FitPortal.Areas.Admin.Models;
using FitPortal.Models.Domain;
using FitPortal.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class AdminHomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPostRepository postRepository;
        private readonly ITeacherRepository teacherRepository;
        private readonly IStudentRepository studentRepository;
        public AdminHomeController(UserManager<ApplicationUser> userManager,IPostRepository postRepository ,ITeacherRepository teacherRepository, IStudentRepository studentRepository)
        {
            this._userManager = userManager;
            this.teacherRepository = teacherRepository;
            this.studentRepository = studentRepository;
            this.postRepository = postRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var posts = await postRepository.GetAll().ToListAsync();
                var teachers = await teacherRepository.GetAll().ToListAsync();
                var students = await studentRepository.GetAll().ToListAsync();
                var users = await _userManager.Users.ToListAsync();
                IndexViewModel model = new IndexViewModel()
                {
                    UserCount = users.Count(),
                    TeacherCount = teachers.Count(),
                    StudentCount = students.Count(),
                    PostCount = posts.Count()
                };
                return View(model);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Index", "Home",new {area = ""});
            }
            
        }
    }
}
