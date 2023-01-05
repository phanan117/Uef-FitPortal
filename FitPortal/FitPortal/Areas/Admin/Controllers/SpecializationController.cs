using FitPortal.Areas.Admin.Models;
using FitPortal.Models.Domain;
using FitPortal.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FitPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class SpecializationController : Controller
    {
        private readonly ISpecializationRepository _specializationRepository;
        private readonly ITeacherRepository _teacherRepository;

        public SpecializationController(ISpecializationRepository specializationRepository, ITeacherRepository teacherRepository)
        {
            this._specializationRepository = specializationRepository;
            this._teacherRepository = teacherRepository;
        }
        [HttpGet]
        public async Task<IActionResult> ViewAll()
        {
            var specializations = _specializationRepository.GetAll().ToList();
            var teacher = _teacherRepository.GetAll().ToList();
            var list = (from p in specializations
                         join t in teacher on p.ManagerID equals t.Id
                         select new
                         {
                             Id = p.Id,
                             SpecializationID = p.SpecializationID,
                             SpecializationName = p.SpecializationName,
                             ManagerName = t.Name,
                             DateCreate = p.DateCreate
                         }).ToList();
            List<SpecializationViewModel> model = new List<SpecializationViewModel>();
            foreach(var item in list)
            {
                SpecializationViewModel data = new SpecializationViewModel();
                data.ID = item.Id;
                data.SpecializationID = item.SpecializationID;
                data.SpecializationName = item.SpecializationName;
                data.ManagerName = item.ManagerName;
                data.DateCreate = item.DateCreate;
                model.Add(data);
            }
            return View(model);
        }
        //Lấy hết giảng viên đang công tác để chọn trưởng ngành
        [HttpGet]
        public IActionResult AddSpecialization()
        {
            var teacher = _teacherRepository.GetAll().Where(t => t.IsDeleted == false).ToList();
            SelectList selectListItems = new SelectList(teacher, "Id", "Name");
            ViewBag.Teacher = selectListItems;
            return View();
        }
        //Lấy hết giảng viên đang công tác để chọn trưởng ngành khi muôn sửa đổi
        [HttpGet]
        public IActionResult EditSpecialization(int IDSpecialization)
        {
            var specialization = _specializationRepository.FindById(IDSpecialization);
            SpecializationViewModel model = new SpecializationViewModel 
            { 
                ID=specialization.Id,
                SpecializationName=specialization.SpecializationName,
                SpecializationID=specialization.SpecializationID,
                DateCreate=specialization.DateCreate,
                ManagerID=specialization.ManagerID
            };
            var teacher = _teacherRepository.GetAll().Where(t => t.IsDeleted == false).ToList();
            SelectList selectListItems = new SelectList(teacher, "Id", "Name");
            ViewBag.Teacher = selectListItems;
            return View(model);
        }
        [HttpPost]
        public IActionResult AddSpecialization(SpecializationViewModel model)
        {
            if (ModelState.IsValid)
            {
                Specialization specialization = new Specialization
                {
                    SpecializationID = model.SpecializationID,
                    SpecializationName = model.SpecializationName,
                    DateCreate = model.DateCreate,
                    ManagerID = model.ManagerID,
                };
                var result = _specializationRepository.Create(specialization);
                if (result == false)
                {
                    ViewData["msg"] = "Thêm mới thất bại";
                    return View(model);
                }
            }
            return RedirectToAction("ViewAll","Specialization");
        }
        [HttpPost]
        public IActionResult EditSpecialization(SpecializationViewModel model)
        {
            var specialization = _specializationRepository.GetAll().Where(m => m.Id == model.ID).FirstOrDefault();
            specialization.SpecializationID = model.SpecializationID;
            specialization.SpecializationName = model.SpecializationName;
            specialization.DateCreate = model.DateCreate;
            specialization.ManagerID = model.ManagerID;
            var result = _specializationRepository.Update(specialization);
            if(result == false)
            {
                TempData["msg"] = "Một vài dữ liệu không hợp lệ bạn vui lòng kiểm tra lại!";
                return View(model);
            }
            return RedirectToAction("ViewAll", "Specialization");
        }
    }
}
