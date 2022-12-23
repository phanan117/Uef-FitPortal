using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ClassController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
