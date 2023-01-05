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
    public class TeacherController : Controller
    {
        //Dung repositoty custom de crud du lieu
        private readonly ITeacherRepository _teacherRepository;
        private readonly IWebHostEnvironment _webHostEnvironment; 
        private readonly ITeacherPositionRepository _positionRepository; //trung gian ket noi khoa chinh cua giang vien & chuyen nganh
        private readonly ISpecializationRepository _SpecializationRepository;
        private readonly DatabaseContext _dbcon;
        public TeacherController(ITeacherRepository teacherRepository, IWebHostEnvironment webHostEnvironment, ITeacherPositionRepository positionRepository, ISpecializationRepository specializationRepository, DatabaseContext databaseContext)
        {
            this._teacherRepository = teacherRepository;
            this._webHostEnvironment = webHostEnvironment;
            this._positionRepository = positionRepository;
            this._SpecializationRepository = specializationRepository;
            this._dbcon = databaseContext;
        }
        
        //Lay danh sach giang vien
        [HttpGet]
        public IActionResult ViewAll()
        {
            List<Teachers> model = _teacherRepository.GetAll().ToList();
            return View(model);
        }

        //Trả về view
        [HttpGet]
        public IActionResult AddTeacher()
        {
            return View();
        }

        //lay chuyen nganh cho giang vien
        [HttpGet]
        public async Task<IActionResult> TeacherPosition()
        {
            var teachers = await _teacherRepository.GetAll().ToListAsync();
            var position = await _positionRepository.GetAll().ToListAsync();
            List<TeacherPositionViewModel> model = new List<TeacherPositionViewModel>();
            foreach (var teacher in teachers)
            {
                //duyet id cua giang vien trong bang vi tri xem giang vien da co chuyen nganh chua?
                var havePosition = (from p in position
                                    where p.TeacherID == teacher.Id
                                    select new{ID = p.SpecializationID}).FirstOrDefault();
                //co chuyen nganh
                if(havePosition != null)
                {
                    var specialization = _SpecializationRepository.GetAll().Where(s => s.Id == havePosition.ID).FirstOrDefault();
                    if(specialization != null)
                    {
                        var teacherManager = await _teacherRepository.GetAll().Where(t => t.Id == specialization.ManagerID).FirstOrDefaultAsync();
                        if(teacherManager != null)
                        {
                            TeacherPositionViewModel itemModelHavePosition = new TeacherPositionViewModel()
                            {
                                ID = teacher.Id,
                                Name = teacher.Name,
                                SpecializationName = specialization.SpecializationName,
                                Manager = teacherManager.Name
                            };
                            model.Add(itemModelHavePosition);
                        }
                    }
                }
                // chua co chuyen nganh
                else
                {
                    TeacherPositionViewModel itemModel = new TeacherPositionViewModel()
                    {
                        ID = teacher.Id,
                        Name = teacher.Name,
                        SpecializationName = "Chưa vào chuyên ngành",
                        Manager = "Chưa có trưởng ngành"
                    };
                    model.Add(itemModel);
                }
                
            }
            return View(model);
        }
        [HttpGet] 
        public async Task<IActionResult> AddTeacherToPosition(int IDTeacher)
        {
            var specialization = await _SpecializationRepository.GetAll().ToListAsync();
            SelectList selectListItems = new SelectList(specialization,"Id","SpecializationName");
            //Truyền ID để biết được đang update cho Teacher nào
            ViewBag.TeacherID = IDTeacher;
            ViewBag.Specialization = selectListItems;
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
            //Dùng viewbag để lấy đường dẫn hình ảnh
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
                //Nếu có thay đổi hình ảnh thì tìm hình ảnh cũ và xóa
                if (DeleteImg(teacher.Avatar) == true)
                {
                    string folder = "Images/Teacher/";
                    folder += Guid.NewGuid().ToString() + "_" + model.Avatar.FileName;
                    teacher.Avatar = "/" + folder;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    using (FileStream fs = new FileStream(serverFolder, FileMode.Create))
                    {
                        await model.Avatar.CopyToAsync(fs);
                    }
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
                var result = false;
                try
                {
                    result = _teacherRepository.Update(teacher);
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                
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
        //Tạo mới đối tượng teacher lưu vào csdl
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
                    using (FileStream fs = new FileStream(serverFolder, FileMode.Create))
                    {
                        await model.Avatar.CopyToAsync(fs);
                    }
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTeacherToPosition(AddTeacherToSpecializationViewModel model)
        {
            if (ModelState.IsValid)
            {
                //kiem tra giang vien co chuyen nganh chua
                var teacherP = _positionRepository.GetAll().Where(p => p.TeacherID == model.TeacherId).FirstOrDefault();
                //neu co chuyen nganh roi thi update
                if (teacherP != null)
                {
                    var position = _positionRepository.GetAll().Where(p => p.TeacherID == teacherP.TeacherID).FirstOrDefault();
                    if(position != null)
                    {
                        position.TeacherID = model.TeacherId;
                        position.SpecializationID = model.SpecializationId;
                        var result = _positionRepository.Update(position);
                        if (result)
                        {
                            return RedirectToAction("TeacherPosition", "Teacher");
                        }
                    }
                }
                //nếu chưa có chuyên ngành thì create
                else
                {
                    var position = _positionRepository.Create(new TeacherPosition { SpecializationID = model.SpecializationId, TeacherID = model.TeacherId });
                    var duplicateFix =await _positionRepository.GetAll().Where(p => p.TeacherID == model.TeacherId).ToListAsync();
                    //nếu có >1 mã số giảng viên bị trùng thì delete & break để chỉ xóa 1.
                    if(duplicateFix.Count > 1)
                    {
                        foreach (var item in duplicateFix)
                        {
                            _positionRepository.Delete(item);
                            break;
                        }
                    }
                    if (position)
                    {
                        return RedirectToAction("TeacherPosition", "Teacher");
                    }
                }
            }
            return RedirectToAction("TeacherPosition", "Teacher");
        }
    }
}