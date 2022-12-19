using Microsoft.AspNetCore.Mvc;

namespace FitPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpacializationController : Controller
    {
        public IActionResult Information()
        {
            return View();
        }
        public IActionResult AddSpecialization()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddSpecialization(string input)
        {
            return View();
        }
    }
}
