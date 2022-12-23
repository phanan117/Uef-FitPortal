using FitPortal.Areas.Admin.Models;
using FitPortal.Models.Domain;
using FitPortal.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class StudentController : Controller
    {
        private readonly IStudentRepository studentRepository;
        public StudentController(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }
        //Get
        [HttpGet]
        public IActionResult ViewAll()
        {
            var model = studentRepository.GetAll().Where(t=>t.IsDeleted==false).ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult AddStudent()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Graduating()
        {
            var model = studentRepository.GetAll().Where(t => t.IsDeleted==true).ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult EditStudent(int IDStudent)
        {
            var student = studentRepository.GetAll().Where(t => t.Id == IDStudent).FirstOrDefault();
            if(student != null)
            {
                EditStudentViewModel model = new EditStudentViewModel()
                {
                    Id = student.Id,
                    Name = student.Name,
                    Gender = student.Gender,
                    DayOfBirth = student.DayOfBirth,
                    PhoneNumber = student.PhoneNumber,
                    Address = student.Address,
                    Email = student.Email,
                    StudentCode = student.StudentCode
                };
                return View(model);
            }
            return View(nameof(ViewAll));
        }
        [HttpGet]
        public IActionResult DeleteStudent(int IDStudent)
        {
            var student = studentRepository.GetAll().Where(t => t.Id ==IDStudent).FirstOrDefault();
            if(student!= null)
            {
                student.IsDeleted = true;
                var result = studentRepository.Update(student);
                if (result)
                {
                    return RedirectToAction("ViewAll", "Student");
                }
            }
            return RedirectToAction("ViewAll", "Student");
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddStudent(AddStudentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                Students student = new Students()
                {
                    Name = model.Name,
                    DayOfBirth = model.DayOfBirth,
                    Gender = model.Gender,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    Address = model.Address,
                    StudentCode = model.StudentCode,
                    CreateDate = DateTime.Now,
                    LastModify = DateTime.Now,
                    IsDeleted = false
                };
                var result = studentRepository.Create(student);
                if (result==false)
                {
                    return View(model);
                }
                else
                {
                    return RedirectToAction("ViewAll", "Student");
                }
            }
            
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditStudent(EditStudentViewModel model)
        {
            var student = studentRepository.GetAll().Where(t => t.Id == model.Id).FirstOrDefault();
            if (student != null)
            {
                student.Name = model.Name;
                student.DayOfBirth = model.DayOfBirth;
                student.Gender = model.Gender;
                student.PhoneNumber = model.PhoneNumber;
                student.Email = model.Email;
                student.Address = model.Address;
                student.StudentCode = model.StudentCode;
                student.LastModify = DateTime.Now;
                var result = studentRepository.Update(student);
                if (result == false)
                {
                    return View(model);
                }
                else return RedirectToAction("ViewAll", "Student");
            }
            return RedirectToAction("ViewAll", "Student");
        }
    }
}
