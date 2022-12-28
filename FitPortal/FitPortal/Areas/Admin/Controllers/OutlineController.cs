﻿using FitPortal.Areas.Admin.Models;
using FitPortal.Models.Domain;
using FitPortal.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OutlineController : Controller
    {
        private readonly IOutlineRepository outlineRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ISubjectRepository subjectRepository;
        public OutlineController(IOutlineRepository outlineRepository, IWebHostEnvironment webHostEnvironment,ISubjectRepository subjectRepository)
        {
            this.outlineRepository = outlineRepository;
            _webHostEnvironment = webHostEnvironment;
            this.subjectRepository = subjectRepository;
        }
        [HttpGet]
        public IActionResult AddOutline(int IDSubject)
        {
            ViewBag.IDSubject = IDSubject;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditOutline(int IDOutline)
        {
            try
            {
                var outline =await outlineRepository.GetAll().Where(o => o.Id == IDOutline).FirstOrDefaultAsync();
                if(outline != null)
                {
                    EditOutlineViewModel model = new EditOutlineViewModel() 
                    {
                        Name = outline.Name,
                        FileName = outline.File,
                        IdOutline = outline.Id
                    };
                    return View(model);
                }
                else
                {
                    return View();
                }
                
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("ViewAll", "Subject");
            }
        }
        [HttpGet]
        public async Task<IActionResult> ViewAll(int IDSubject)
        {
            try
            {
                var outline = await outlineRepository.GetAll().Where(o => o.SubjectId == IDSubject).ToListAsync();
                List<OutlineViewModel> model = new List<OutlineViewModel>();
                foreach (var item in outline)
                {
                    OutlineViewModel itemModel = new OutlineViewModel()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        File = item.File
                    };
                    model.Add(itemModel);
                }
                ViewBag.IDSubject = IDSubject;
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("ViewAll", "Subject");
            }
        }
        //Fucntion
        public bool DeleteFile(string path)
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
        public async Task<IActionResult> AddOutline(AddOutlineViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Outline outline = new Outline();
                    string folder = "file/outline/";
                    folder += Guid.NewGuid().ToString() + "_" + model.File.FileName;
                    outline.File = "/" + folder;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    await model.File.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                    //add
                    outline.Name = model.Name;
                    outline.SubjectId = model.IdSubject;
                    var result = outlineRepository.Create(outline);
                    if (result)
                    {
                        return RedirectToAction("ViewAll","Outline", new {IDSubject = model.IdSubject});
                    }
                    else
                    {
                        return View(nameof(ViewAll), new { IDSubject = model.IdSubject });
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return RedirectToAction("ViewAll", "Subject");
                }
            }
            else
            {
                ViewBag.IDSubject = model.IdSubject;
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditOutline(EditOutlineViewModel model)
        {
            if (ModelState.IsValid)
            {
                var outline =await outlineRepository.GetAll().Where(o => o.Id == model.IdOutline).FirstOrDefaultAsync();
                if(outline != null)
                {
                    outline.Name = model.Name;
                    if(model.File != null)
                    {
                        if (DeleteFile(outline.File) == true)
                        {
                            string folder = "file/outline/";
                            folder += Guid.NewGuid().ToString() + "_" + model.File.FileName;
                            outline.File = "/" + folder;
                            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                            await model.File.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                        }
                    }
                    var result = outlineRepository.Update(outline);
                    if (result)
                    {
                        return RedirectToAction("ViewAll", "Outline", new {IDSubject = outline.SubjectId});
                    }
                }
                return RedirectToAction("ViewAll", "Subject");
            }
            else
            {
                return RedirectToAction("ViewAll", "Subject");
            }
        }
    }
}
