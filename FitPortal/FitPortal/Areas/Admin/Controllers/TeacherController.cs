using FitPortal.Areas.Admin.Models;
using FitPortal.Models.Domain;
using FitPortal.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeacherController : Controller
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public TeacherController(ITeacherRepository teacherRepository, IWebHostEnvironment webHostEnvironment)
        {
            this._teacherRepository = teacherRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult ViewAll()
        {
            List<Teachers> list = _teacherRepository.GetAll().ToList();
            return View(list);
        }
        public IActionResult AddTeacher()
        {
            return View();
        }
        public async Task<IActionResult> EditTeacher(int IDTeacher)
        {
            var teacher = await _teacherRepository.GetAll().Where(t => t.Id == IDTeacher).FirstOrDefaultAsync();
            TeacherViewModel model = new TeacherViewModel
            {
                Name = teacher.Name,
                ID = teacher.Id,
            };
            ViewBag.Avatar=teacher.Avatar;
            return View(teacher);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTeacher(TeacherViewModel model)
        {
            if (ModelState.IsValid)
            {
                var teacher = new Teachers();
                if (model.Avatar != null)
                {
                    string folder = "Images/Teacher/";
                    folder += Guid.NewGuid().ToString() + "_" + model.Avatar.FileName;
                    teacher.Avatar = "/" + folder;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    await model.Avatar.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                }
                teacher.Address = model.Address;
                teacher.Name = model.Name;
                teacher.Gender = model.Gender;
                teacher.DayOfBirth = model.DayOfBirth;
                teacher.CreatedDate = DateTime.Now;
                teacher.TeacherCode = model.TeacherCode;
                teacher.Email = model.Email;
                teacher.PhoneNumber = model.PhoneNumber;
                teacher.Identification = model.Identification;
                teacher.LastUpdatedDate = DateTime.Now;
                teacher.IsDeleted = false;
                _teacherRepository.Create(teacher);
            }
            else
            {
                return View(model);
            }
            return RedirectToAction("ViewAll","Teacher");
        }
    }
}
