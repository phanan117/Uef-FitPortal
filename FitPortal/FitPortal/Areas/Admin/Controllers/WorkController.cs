using FitPortal.Areas.Admin.Models;
using FitPortal.Models.Domain;
using FitPortal.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FitPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class WorkController : Controller
    {
        private readonly IWorkRepository workRepository;
        private readonly ITeacherRepository teacherRepository;
        private readonly ITeacherWorkRepository teacherWorkRepository;
        private readonly DatabaseContext dbcon;
        public WorkController(IWorkRepository workRepository, ITeacherRepository teacherRepository,DatabaseContext databaseContext, ITeacherWorkRepository teacherWorkRepository)
        {
            this.workRepository = workRepository;
            this.teacherRepository = teacherRepository;
            this.dbcon = databaseContext;
            this.teacherWorkRepository = teacherWorkRepository;
        }
        [HttpGet]
        public IActionResult ViewAll()
        {
            var works = workRepository.GetAll().ToList();
            List<WorkViewModel> model = new List<WorkViewModel>();
            foreach (var work in works)
            {
                WorkViewModel modelItem = new WorkViewModel()
                {
                    Id = work.Id,
                    Name = work.Name,
                    DateCreate = work.DateCreate,
                    LastModify = work.LastMofify
                };
                model.Add(modelItem);
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult AddWork()
        {
            return View();
        }
        [HttpGet]
        public IActionResult EditWork(int IDWork)
        {
            var work = workRepository.GetAll().Where(w => w.Id == IDWork).FirstOrDefault();
            EditWorkViewModel model = new EditWorkViewModel();
            if (work != null)
            {
                model.Id = work.Id;
                model.Name = work.Name;
                model.Address = work.Address;
                model.DateEnd = work.DateEnd;
                model.DateStart = work.DateStart;
                model.Description = work.Description;
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult DetailWork(int IDWork)
        {
            var work = workRepository.GetAll().Where(w => w.Id == IDWork).FirstOrDefault();
            if(work != null)
            {
                return View(work);
            }
            return RedirectToAction("ViewAll","Work");
        }
        [HttpGet]
        public IActionResult WorkStatus()
        {
            var works = workRepository.GetAll().ToList();
            List<WorkStatusViewModel> model = new List<WorkStatusViewModel>();
            foreach(var work in works)
            {
                WorkStatusViewModel itemModel = new WorkStatusViewModel()
                {
                    Id =work.Id,
                    Name = work.Name,
                    DateStart = work.DateStart,
                    DateEnd = work.DateEnd,
                    IsTaked = work.IsTaked
                };
                model.Add(itemModel);
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Assignment(int IDWork)
        {
            ViewBag.IDWork = IDWork;
            var teacher = teacherRepository.GetAll().ToList();
            SelectList selectListItems = new SelectList(teacher,"Id","Name");
            ViewBag.Teacher = selectListItems;
            return View();
        }
        [HttpGet]
        public IActionResult TeacherAssign()
        {
            var teachers = teacherRepository.GetAll().ToList();
            List<TeacherAssignViewModel> model = new List<TeacherAssignViewModel>();
            foreach(var teacher in teachers)
            {
                TeacherAssignViewModel itemModel = new TeacherAssignViewModel()
                {
                    Id=teacher.Id,
                    Name=teacher.Name,
                    Email=teacher.Email,
                };
                model.Add(itemModel);
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult WorkingSchedule(int IDTeacher)
        {
            var teacher = teacherRepository.GetAll().Where(T => T.Id == IDTeacher).FirstOrDefault();
            ViewBag.TeacherName = teacher.Name;
            var teacherWorks = teacherWorkRepository.GetAll().Where(t => t.TeachersId==IDTeacher).ToList();
            List<WorkingScheduleViewModel> model = new List<WorkingScheduleViewModel>();
            foreach(var teachersWork in teacherWorks)
            {
                var work = workRepository.GetAll().Where(w => w.Id==teachersWork.WorksId).ToList();
                if (work != null)
                {
                    foreach(var item in work)
                    {
                        WorkingScheduleViewModel itemModel = new WorkingScheduleViewModel()
                        {
                            WorkName = item.Name,
                            StartDate = item.DateStart,
                            EndDate = item.DateEnd,
                            Address = item.Address
                        };
                        model.Add(itemModel);
                    }
                }
                else
                {
                    return View(model);
                }
            }
            return View(model);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Assignment(AssignmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                var teacherWork = new TeachersWorks()
                {
                    TeachersId = model.IDTeacher,
                    WorksId = model.IdWork
                };
                var result = teacherWorkRepository.Create(teacherWork);
                if(result)
                {
                    var fixList = teacherWorkRepository.GetAll().Where(t => t.WorksId == model.IdWork).ToList();
                    if (fixList.Count > 1)
                    {
                        foreach(var item in fixList)
                        {
                            var fix = teacherWorkRepository.GetAll().Where(t => t.WorksId == model.IdWork).FirstOrDefault();
                            if(fix != null) teacherWorkRepository.Delete(fix);
                            break;
                        }
                        var work = workRepository.GetAll().Where(w => w.Id == model.IdWork).FirstOrDefault();
                        if(work != null)
                        {
                            work.IsTaked = true;
                            workRepository.Update(work);
                        }
                    }
                    else
                    {
                        var work = workRepository.GetAll().Where(w => w.Id == model.IdWork).FirstOrDefault();
                        if (work != null)
                        {
                            work.IsTaked = true;
                            workRepository.Update(work);
                        }
                    }
                    return RedirectToAction("WorkStatus","Work");
                }
                else
                {
                    return RedirectToAction("AdminHome", "Index");
                }
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddWork(AddWorkViewModel model)
        {
            if (ModelState.IsValid)
            {
                Works work = new Works() 
                {
                    Name = model.Name,
                    Description = model.Description,
                    Address = model.Address,
                    DateStart = model.DateStart,
                    DateEnd = model.DateEnd,
                    DateCreate = DateTime.Now,
                    LastMofify = DateTime.Now,
                    Status = false,
                    IsTaked = false
                };
                var result = workRepository.Create(work);
                if (result)
                {
                    return RedirectToAction("ViewAll","Work");
                }
            }
            else
            {
                return View(model);
            }
            return RedirectToAction("ViewAll", "Work");
        }
        [HttpPost]
        public IActionResult EditWork(EditWorkViewModel model)
        {
            if (ModelState.IsValid)
            {
                var work = workRepository.GetAll().Where(w => w.Id == model.Id).FirstOrDefault();
                if(work != null)
                {
                    work.Name = model.Name;
                    work.Description = model.Description;
                    work.Address = model.Address;
                    work.DateStart = model.DateStart;
                    work.DateEnd = model.DateEnd;
                    work.DateCreate = DateTime.Now;
                    work.LastMofify = DateTime.Now;
                    var result = workRepository.Update(work);
                    if (result)
                    {
                        return RedirectToAction("ViewAll", "Work");
                    }
                } 
            }
            else
            {
                return View(model);
            }
            return RedirectToAction("ViewAll", "Work");
        }
    }
}
