using FitPortal.Models;
using FitPortal.Models.Domain;
using FitPortal.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FitPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DatabaseContext _dbcon;
        public HomeController(ILogger<HomeController> logger, DatabaseContext dbcon)
        {
            this._logger = logger;
            this._dbcon = dbcon;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var events = await (from posts in _dbcon.Posts
                          join category in _dbcon.Categories on posts.CategoryID equals category.Id
                          where category.CategoryName == "Sự kiện" && posts.IsDisplay == true
                          select new
                          {
                              ID = posts.Id,
                              Title = posts.PostName,
                              CreateTime = posts.DateCreated,
                              Content = posts.Content,
                              Picture = posts.Picture,
                              Describe = posts.Describe
                          }).ToListAsync();
            List<EventInformation> listEvent = new List<EventInformation>();
            foreach(var item in events)
            {
                EventInformation inforEvent = new EventInformation();
                inforEvent.Id=item.ID;
                inforEvent.Title=item.Title;
                inforEvent.CreateTime=item.CreateTime;
                inforEvent.Content=item.Content;
                inforEvent.Picture=item.Picture;
                inforEvent.Describe = item.Describe;
                listEvent.Add(inforEvent);
            }
            ViewBag.Events = listEvent;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}