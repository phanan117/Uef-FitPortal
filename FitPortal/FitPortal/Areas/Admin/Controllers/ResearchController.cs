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
    public class ResearchController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly IResearchRepository researchRepository;
        private readonly IStudentResearchRepository studentResearchRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ResearchController(IStudentRepository studentRepository, IWebHostEnvironment webHostEnvironment, IResearchRepository researchRepository, IStudentResearchRepository studentResearchRepository)
        {
            this.studentRepository = studentRepository;
            this.researchRepository = researchRepository;
            this.studentResearchRepository = studentResearchRepository;
            this._webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> Student()
        {
            try
            {
                var students = await studentRepository.GetAll().ToListAsync();
                List<StudentResearchViewModel> model = new List<StudentResearchViewModel>();
                foreach(var student in students)
                {
                    StudentResearchViewModel itemModel = new StudentResearchViewModel()
                    {
                        Id = student.Id,
                        Name = student.Name,
                        StudentCode = student.StudentCode,
                        Email = student.Email
                    };
                    model.Add(itemModel);
                }
                return View(model);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            
        }
        [HttpGet]
        public async Task<IActionResult> ViewResearch(int IDStudent)
        {
            try
            {
                var student =await studentRepository.GetAll().Where(s => s.Id == IDStudent).FirstOrDefaultAsync();
                if(student != null)
                {
                    List<ViewResearchViewModel> model = new List<ViewResearchViewModel>();  
                    var studentRs = await studentResearchRepository.GetAll().Where(sr => sr.StudentId == IDStudent).ToListAsync();
                    foreach(var item in studentRs)
                    {
                        var research = researchRepository.GetAll().Where(r => r.Id == item.ResearchId).FirstOrDefault();
                        ViewResearchViewModel itemModel = new ViewResearchViewModel()
                        {
                            Id = research.Id,
                            Name = research.Name,
                            NameEnglish = research.NameEnglish,
                            DateStart = research.DateStart,
                            DateEnd = research.DateEnd
                        };
                        model.Add(itemModel);
                    }
                    ViewBag.StudentName = student.Name;
                    ViewBag.StudentID = student.Id;
                    return View(model);
                }
                else
                {
                    return View(nameof(Student));
                }
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            
        }
        [HttpGet]
        public IActionResult AddForStudent(int IDStudent)
        {
            ViewBag.IDStudent = IDStudent;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditForStudent(int IDResearch)
        {
            var research = await researchRepository.GetAll().Where(r => r.Id == IDResearch).FirstOrDefaultAsync();
            if(research != null)
            {
                EditForStudentViewModel model = new EditForStudentViewModel()
                {
                    ResearchId = research.Id,
                    Name = research.Name,
                    DateEnd=research.DateEnd,
                    DateStart = research.DateStart,
                    NameEnglish = research.NameEnglish
                };
                return View(model);
            }
            else
            {
                var sr = await studentResearchRepository.GetAll().Where(sr => sr.ResearchId == IDResearch).FirstOrDefaultAsync();
                return RedirectToAction("ViewResearch", "Research", new { IDStudent = sr.StudentId });
            }
            
        }
        //Methot
        protected bool DeleteFile(string path)
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
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddForStudent(AddForStudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Research research = new Research();
                    string folder = "file/research/";
                    folder += Guid.NewGuid().ToString() + "_" + model.formFile.FileName;
                    research.File = "/" + folder;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    await model.formFile.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                    research.Name = model.Name;
                    research.NameEnglish = model.NameEnglish;
                    research.DateStart = model.DateStart;
                    research.DateEnd = model.DateEnd;
                    bool result = false;
                    result = researchRepository.Create(research);
                    if (result)
                    {
                        try
                        {
                            var researchInfo = await researchRepository.GetAll().Where(rs => rs.NameEnglish == model.NameEnglish).FirstOrDefaultAsync();
                            if(researchInfo != null)
                            {
                                StudentResearch sr = new StudentResearch()
                                {
                                    StudentId = model.Id,
                                    ResearchId = researchInfo.Id
                                };
                                result = studentResearchRepository.Create(sr);
                                if (result)
                                {
                                    return RedirectToAction("ViewResearch", "Research", new { IDStudent = model.Id });
                                }
                                else
                                {
                                    return RedirectToAction("Student", "Research");
                                }
                            }
                            else
                            {
                                return RedirectToAction("Student", "Research");
                            }
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            return RedirectToAction("Student", "Research");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Student", "Research");
                    }
                    
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return RedirectToAction("Index", "Home", new { area = "" });
                }               
            }
            else
            {
                ViewBag.IDStudent = model.Id;
                return View(model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditForStudent(EditForStudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var research = await researchRepository.GetAll().Where(rs => rs.Id == model.ResearchId).FirstOrDefaultAsync();
                if(research != null)
                {
                    if(model.formFile != null)
                    {
                        if (DeleteFile(research.File) == true)
                        {
                            string folder = "file/research/";
                            folder += Guid.NewGuid().ToString() + "_" + model.formFile.FileName;
                            research.File = "/" + folder;
                            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                            using (FileStream fs = new FileStream(serverFolder, FileMode.Create))
                            {
                                await model.formFile.CopyToAsync(fs);
                            }
                        }
                    }
                    research.Name = model.Name;
                    research.NameEnglish = model.NameEnglish;
                    research.DateStart = model.DateStart;
                    research.DateEnd = model.DateEnd;
                    try
                    {
                        var result = researchRepository.Update(research);
                        if (result)
                        {
                            var sr = await studentResearchRepository.GetAll().Where(sr => sr.ResearchId == model.ResearchId).FirstOrDefaultAsync();
                            return RedirectToAction("ViewResearch", "Research", new { IDStudent = sr.StudentId });
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return RedirectToAction("Index", "Home", new { area = "" });
                    }
                }
            }
            return View(model);
        }
    }
}
