using FitPortal.Areas.Admin.HtmlHelper;
using FitPortal.Areas.Admin.Models;
using FitPortal.Models.Domain;
using FitPortal.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static FitPortal.Areas.Admin.HtmlHelper.Helper;

namespace FitPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SubjectController : Controller
    {
        private readonly ISubjectRepository subjectRepository;
        private readonly ISpecializationRepository specializationRepository;
        private readonly ISubjectMajorsRepository subjectMajorsRepository;
        //Constructor
        public SubjectController(ISubjectRepository subjectRepository, ISpecializationRepository specializationRepository, ISubjectMajorsRepository subjectMajorsRepository)
        {
            this.subjectRepository = subjectRepository;
            this.specializationRepository = specializationRepository;
            this.subjectMajorsRepository = subjectMajorsRepository;
        }
        //Get
        [HttpGet]
        public IActionResult ViewAll()
        {
            List<SubjectViewModel> model = new List<SubjectViewModel>();
            try
            {
                var subjects = subjectRepository.GetAll().ToList();
                if (subjects.Count > 0)
                {
                    foreach (var subject in subjects)
                    {
                        //One - one 
                        var majorsId = subjectMajorsRepository.GetAll().Where(s => s.SubjectId == subject.Id).FirstOrDefault();
                        if (majorsId != null)
                        {
                            var majorsInfo = specializationRepository.GetAll().Where(s => s.Id == majorsId.MajorsId).FirstOrDefault();
                            if (majorsInfo != null)
                            {
                                SubjectViewModel itemModel = new SubjectViewModel()
                                {
                                    SubjectId = subject.Id,
                                    SubjectCode = subject.SubjectCode,
                                    SubjectName = subject.SubjectName,
                                    SpecializationName = majorsInfo.SpecializationName
                                };
                                model.Add(itemModel);
                            }
                        }
                        else
                        {
                            SubjectViewModel itemModel = new SubjectViewModel()
                            {
                                SubjectId = subject.Id,
                                SubjectCode = subject.SubjectCode,
                                SubjectName = subject.SubjectName,
                                SpecializationName = "Không có"
                            };
                            model.Add(itemModel);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View(model);
        }
        [NoDirectAccess]
        public IActionResult AddSubject()
        {
            var majors = specializationRepository.GetAll().ToList();
            SelectList selectListItems = new SelectList(majors, "Id", "SpecializationName");
            ViewBag.Specialization = selectListItems;
            return View();
        }
        [HttpGet]
        public IActionResult DeleteSubject(int IDSubject)
        {
            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSubject(AddSubjectViewModel model)
        {
            if(ModelState.IsValid)
            {
                var check = subjectRepository.GetAll().Where(s => s.SubjectCode == model.SubjectCode).ToList();
                if (check.Count > 0)
                {
                    var majors = specializationRepository.GetAll().ToList();
                    SelectList selectListItems = new SelectList(majors, "Id", "SpecializationName");
                    ViewBag.Specialization = selectListItems;
                    TempData["msg"] = "Mã môn học bị trùng vui lòng thay đổi!";
                    return View(model);
                }
                else
                {
                    try
                    {
                        Subject subject = new Subject()
                        {
                            SubjectName = model.SubjectName,
                            SubjectCode = model.SubjectCode,
                            CreatedDate = DateTime.Now,
                            LastModify = DateTime.Now
                        };
                        if (subjectRepository.Create(subject))
                        {
                            var fixDuplicate = subjectRepository.GetAll().Where(s => s.SubjectCode == model.SubjectCode).ToList();
                            if(fixDuplicate.Count > 1)
                            {
                                foreach(var fix in fixDuplicate)
                                {
                                    subjectRepository.Delete(fix);
                                    break;
                                }
                            }
                            var subjectNew = subjectRepository.GetAll().Where(s => s.SubjectCode == model.SubjectCode).FirstOrDefault();
                            if (subjectNew != null)
                            {
                                SubjectMajors sm = new SubjectMajors()
                                {
                                    SubjectId = subjectNew.Id,
                                    MajorsId = model.MajorId
                                };
                                try
                                {
                                    subjectMajorsRepository.Create(sm);
                                }catch(Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                
                            }
                            else
                            {
                                TempData["msg"] = "Thêm mới thất bại!";
                                return RedirectToAction("ViewAll", "Subject");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            //Để render View có sử dụng dropdown list cần phải truyền dữ liệu
            //của dropdownList cho view trước khi render
            var temp = specializationRepository.GetAll().ToList();
            SelectList selectListItemsTemp = new SelectList(temp, "Id", "SpecializationName");
            ViewBag.Specialization = selectListItemsTemp;
            var ketqua = Helper.RenderRazorViewToString(this, "AddSubject", model);
            return Json(new { isValid = false, html = ketqua });
        }
    }
}
