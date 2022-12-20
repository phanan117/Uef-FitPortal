using FitPortal.Areas.Admin.Models;
using FitPortal.Models.Domain;
using FitPortal.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class TeacherController : Controller
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public TeacherController(ITeacherRepository teacherRepository, IWebHostEnvironment webHostEnvironment)
        {
            this._teacherRepository = teacherRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult ViewAll()
        {
            List<Teachers> list = _teacherRepository.GetAll().Where(T => T.IsDeleted == false).ToList();
            return View(list);
        }
        [HttpGet]
        public IActionResult ViewResign()
        {
            List<Teachers> list = _teacherRepository.GetAll().Where(T => T.IsDeleted == true).ToList();
            return View(list);
        }
        public IActionResult AddTeacher()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditTeacher(int IDTeacher)
        {
            var teacher = await _teacherRepository.GetAll().Where(t => t.Id == IDTeacher).FirstOrDefaultAsync();
            TeacherViewModel model = new TeacherViewModel
            {
                ID=teacher.Id,
                Name=teacher.Name,
                DayOfBirth=teacher.DayOfBirth,
                Gender=teacher.Gender,
                TeacherCode=teacher.TeacherCode,
                Address=teacher.Address,
                PhoneNumber=teacher.PhoneNumber,
                Email=teacher.Email,
                Identification=teacher.Identification
            };
            ViewBag.Avatar=teacher.Avatar;
            return View(model);
        }
        public bool DeleteImg(string path)
        {
            string curentImg = "wwwroot/" + path;
            FileInfo fi = new FileInfo(curentImg);
            if (fi != null)
            {
                System.IO.File.Delete(curentImg);
                fi.Delete();
                return true;
            }
            return false;
        }
        [HttpGet]
        public IActionResult DeleteTeacher(int IDTeacher)
        {
            var teacher = _teacherRepository.GetAll().Where(T => T.Id == IDTeacher).FirstOrDefault();
            teacher.IsDeleted = true;
            _teacherRepository.Update(teacher);
            return RedirectToAction("ViewAll", "Teacher");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTeacher(TeacherViewModel model, IFormFile avatar)
        {
            var teacher = await _teacherRepository.GetAll().Where(t => t.Id == model.ID).FirstOrDefaultAsync();
            if (avatar != null)
            {
                if (DeleteImg(teacher.Avatar) == true)
                {
                    string folder = "Images/Teacher/";
                    folder += Guid.NewGuid().ToString() + "_" + model.Avatar.FileName;
                    teacher.Avatar = "/" + folder;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    await model.Avatar.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                }
                teacher.TeacherCode = model.TeacherCode;
                teacher.Name = model.Name;
                teacher.Address = model.Address;
                teacher.DayOfBirth = model.DayOfBirth;
                teacher.Gender = model.Gender;
                teacher.PhoneNumber=model.PhoneNumber;
                teacher.Email = model.Email;
                teacher.Identification = model.Identification;
                teacher.LastUpdatedDate = DateTime.Now;
                var result = _teacherRepository.Update(teacher);
                if (result)
                {
                    return RedirectToAction("ViewAll", "Teacher");
                }
                else
                {
                    return RedirectToAction("ViewAll", "Teacher");
                }
            }
            else
            {
                teacher.TeacherCode = model.TeacherCode;
                teacher.Name = model.Name;
                teacher.Address = model.Address;
                teacher.DayOfBirth = model.DayOfBirth;
                teacher.Gender = model.Gender;
                teacher.PhoneNumber = model.PhoneNumber;
                teacher.Email = model.Email;
                teacher.Identification = model.Identification;
                teacher.LastUpdatedDate = DateTime.Now;
                var result = _teacherRepository.Update(teacher);
                if (result)
                {
                    return RedirectToAction("ViewAll", "Teacher");
                }
                else
                {
                    return RedirectToAction("ViewAll", "Teacher");
                }
            }
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
