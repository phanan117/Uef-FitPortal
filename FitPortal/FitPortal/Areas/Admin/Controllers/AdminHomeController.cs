using FitPortal.Areas.Admin.Models;
using FitPortal.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FitPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminHomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminHomeController(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }
        [Authorize]
        public IActionResult Index()
        {
            string? userID = _userManager.GetUserId(HttpContext.User);
            if(userID != null)
            {
                ApplicationUser user = _userManager.FindByIdAsync(userID).Result;
                AdminUserViewModel userDetail = new AdminUserViewModel
                {
                    UserName = user.UserName,
                    ProFilePictrure = user.ProfilePicture
                };
                return View(userDetail);
            }
            return RedirectToAction("Login", "UserAuthentication");
        }
    }
}
