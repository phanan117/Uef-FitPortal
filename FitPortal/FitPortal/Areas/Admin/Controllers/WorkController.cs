using FitPortal.Areas.Admin.Models;
using FitPortal.Models.Domain;
using FitPortal.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class WorkController : Controller
    {
        private readonly IWorkRepository workRepository;
        public WorkController(IWorkRepository workRepository)
        {
            this.workRepository = workRepository;
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
        public IActionResult WorkTeacher()
        {
            //AddView
            return View();
        }
        //POST

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
    }
}
