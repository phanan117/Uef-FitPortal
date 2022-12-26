using FitPortal.Areas.Admin.Models;
using FitPortal.Models.Domain;
using FitPortal.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FitPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class StudentController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly IClassRepository classRepository;
        public StudentController(IStudentRepository studentRepository, IClassRepository classRepository)
        {
            this.studentRepository = studentRepository;
            this.classRepository = classRepository;
        }
        //Get
        [HttpGet]
        public IActionResult ViewAll()
        {
            
            List<StudentViewModel> model = new List<StudentViewModel>();
            try
            {
                
                var students = studentRepository.GetAll().Where(t => t.IsDeleted == false).ToList();
                foreach(var student in students)
                {
                    StudentViewModel itemModel = new StudentViewModel();
                    if (student.ClassID != null)
                    {
                        var classInfo = classRepository.GetAll().Where(c => c.Id == student.ClassID).FirstOrDefault();
                        if(classInfo != null)
                        {
                            itemModel.ClassName = classInfo.ClassCode;
                            itemModel.StudentCode = student.StudentCode;
                            itemModel.StudentName = student.Name;
                            itemModel.Address = student.Address;
                            itemModel.Gender = student.Gender;
                            itemModel.DayOfBirth = student.DayOfBirth;
                            itemModel.PhoneNumber = student.PhoneNumber;
                            itemModel.Email = student.Email;
                            itemModel.StudentId = student.Id;
                            model.Add(itemModel);
                        }
                    }
                    else
                    {
                        itemModel.ClassName = "Not Have Class";
                        itemModel.StudentCode = student.StudentCode;
                        itemModel.StudentName = student.Name;
                        itemModel.Address = student.Address;
                        itemModel.Gender = student.Gender;
                        itemModel.DayOfBirth = student.DayOfBirth;
                        itemModel.PhoneNumber = student.PhoneNumber;
                        itemModel.Email = student.Email;
                        itemModel.StudentId = student.Id;
                        model.Add(itemModel);
                    }
                    
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
        [HttpGet]
        public IActionResult SetClass(int IDStudent)
        {
            var student = studentRepository.GetAll().Where(t => t.Id == IDStudent).FirstOrDefault();
            if(student != null)
            {
                var Clases = classRepository.GetAll().ToList();
                SelectList selectListItems = new SelectList(Clases,"Id","ClassCode");
                ViewBag.StudentId = IDStudent;
                ViewBag.Clases = selectListItems;
                return View();
            }
            else
            {
                return View(nameof(ViewAll));
            }
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SetClass(SetClassViewModel model)
        {
            var student = studentRepository.GetAll().Where(t => t.Id == model.StudentID).FirstOrDefault();
            if(student != null)
            {
                var addToClass = classRepository.GetAll().Where(c => c.Id == model.ClassID).FirstOrDefault();
                if(addToClass != null)
                {
                    if(addToClass.Quantity <= addToClass.Current)
                    {
                        TempData["msg"] = "Lớp đã đủ chỉ số hãy chọn lớp khác hoặc thay đổi danh sách lớp";
                        return RedirectToAction("ViewAll", "Student");
                    }
                    else
                    {
                        student.ClassID = addToClass.Id;
                        var result = false;
                        try
                        {
                            int currnet = addToClass.Current;
                            currnet += 1;
                            addToClass.Current = currnet;
                            if(addToClass.Current == currnet)
                            {
                                classRepository.Update(addToClass);
                            }
                            else
                            {
                                if(addToClass.Current > currnet)
                                {
                                    addToClass.Current -= 1;
                                    classRepository.Update(addToClass);
                                }    
                            }
                            result = studentRepository.Update(student);
                        }catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        if (result)
                        {
                            return RedirectToAction("ViewAll", "Student");
                        }
                        else
                        {
                            TempData["msg"] = "Thêm "+ student.Name +" vào lớp thất bại!";
                            return RedirectToAction("ViewAll", "Student");
                        }
                    }
                }
                else
                {
                    TempData["msg"] = "Không tìm thấy lớp học!";
                    return RedirectToAction("ViewAll", "Student");
                }
            }
            else
            {
                TempData["msg"] = "Lỗi không tìm sinh viên!";
                return RedirectToAction("ViewAll", "Student");
            }
        }
    }
}
