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
    public class ClassController : Controller
    {
        private readonly IClassRepository classRepository;
        private readonly ITeacherRepository teacherRepository;
        private readonly ISpecializationRepository specializationRepository;
        private readonly IStudentRepository studentRepository;
        public ClassController(IClassRepository classRepository, ITeacherRepository teacherRepository, ISpecializationRepository specializationRepository, IStudentRepository studentRepository)
        {
            this.classRepository = classRepository;
            this.teacherRepository = teacherRepository;
            this.specializationRepository = specializationRepository;
            this.studentRepository = studentRepository;
        }
        //Get
        [HttpGet]
        public IActionResult ViewAll()
        {
            List<Class> clases = classRepository.GetAll().ToList();
            List<ClassViewModel> model = new List<ClassViewModel>();
            foreach(var c in clases)
            {
                var teacher = teacherRepository.GetAll().Where(t => t.Id == c.TeacherID).FirstOrDefault();
                var specialization = specializationRepository.GetAll().Where(s => s.Id == c.SpeID).FirstOrDefault();
                if(teacher != null && specialization != null)
                {
                    ClassViewModel itemModel = new ClassViewModel()
                    {
                        Id = c.Id,
                        ClassCode = c.ClassCode,
                        Quantity = c.Quantity,
                        Current = c.Current,
                        Teacher = teacher.Name,
                        SpecializationName = specialization.SpecializationName
                    };
                    model.Add(itemModel);
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult AddClass()
        {
            var teachers = teacherRepository.GetAll().ToList();
            var specializations = specializationRepository.GetAll().ToList();
            SelectList teacherList = new SelectList(teachers,"Id","Name");
            SelectList specializationLisst = new SelectList(specializations,"Id","SpecializationName");
            ViewBag.Teacher = teacherList;
            ViewBag.Specialization = specializationLisst;
            return View();
        }
        [HttpGet]
        public IActionResult EditClass(int IDClass)
        {
            var classInfo = classRepository.GetAll().Where(c => c.Id == IDClass).FirstOrDefault();
            EditClassViewModel model = new EditClassViewModel()
            {
                Id = classInfo.Id,
                ClassCode = classInfo.ClassCode,
                Quantity = classInfo.Quantity,
                TeacherID = classInfo.TeacherID,
                SpecializationID = classInfo.SpeID
            };
            var teachers = teacherRepository.GetAll().ToList();
            var specializations = specializationRepository.GetAll().ToList();
            SelectList teacherList = new SelectList(teachers, "Id", "Name");
            SelectList specializationLisst = new SelectList(specializations, "Id", "SpecializationName");
            ViewBag.Teacher = teacherList;
            ViewBag.Specialization = specializationLisst;
            return View(model);
        }
        [HttpGet]
        public IActionResult SeeList(int IDClass)
        {
            string ClassName = "";
            List<SeeListViewModel> model = new List<SeeListViewModel>();
            try
            {
                var students = studentRepository.GetAll().Where(c => c.ClassID == IDClass).ToList();
                if(students.Count > 0)
                {
                    foreach(var student in students)
                    {
                        SeeListViewModel itemModel = new SeeListViewModel() 
                        {
                            StudentId = student.Id,
                            StudentName = student.Name,
                            StudentCode = student.StudentCode
                        };
                        model.Add(itemModel);
                    }
                }
                var classInfo = classRepository.GetAll().Where(c => c.Id == IDClass).FirstOrDefault();
                if(classInfo != null) ClassName = classInfo.ClassCode;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            ViewBag.ClassCode = ClassName;
            return View(model);
        }
        [HttpGet]
        public IActionResult DeleteStudent(int IDStudent)
        {
            try
            {
                var student = studentRepository.GetAll().Where(t => t.Id == IDStudent).FirstOrDefault();
                if(student != null)
                {
                    var classInfo = classRepository.GetAll().Where(c => c.Id == student.ClassID).FirstOrDefault();
                    if(classInfo != null)
                    {
                        classInfo.Current -= 1;
                        classRepository.Update(classInfo);
                    }
                    student.ClassID = null;
                    studentRepository.Update(student);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("ViewAll", "Class");
        }
        //Update code
        [HttpGet]
        public IActionResult DeleteClass(int IDClass)
        {
            return RedirectToAction("ViewAll", "Class");
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddClass(AddClassViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                Class classNew = new Class()
                {
                    ClassCode = model.ClassCode,
                    Quantity = model.Quantity,
                    TeacherID = model.TeacherID,
                    SpeID = model.SpecializationID,
                    Current = 0
                };
                var result = classRepository.Create(classNew);
                if (result)
                {
                    return RedirectToAction("ViewAll", "Class");
                }else return View(model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditClass(EditClassViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                var classInfo = classRepository.GetAll().Where(c => c.Id == model.Id).FirstOrDefault();
                bool result = false;
                if(classInfo != null)
                {
                    classInfo.ClassCode = model.ClassCode;
                    classInfo.Quantity = model.Quantity;
                    classInfo.TeacherID = model.TeacherID;
                    classInfo.SpeID = model.SpecializationID;
                    result = classRepository.Update(classInfo);
                }
                
                if (result)
                {
                    return RedirectToAction("ViewAll", "Class");
                }
                else return View(model);
            }
        }
    }
}
