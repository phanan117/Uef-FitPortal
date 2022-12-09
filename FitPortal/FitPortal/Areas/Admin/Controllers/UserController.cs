using FitPortal.Areas.Admin.Models.DTO;
using FitPortal.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUserAuthenticationService _authService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserController(IUserAuthenticationService auhtService, IWebHostEnvironment webHostEnvironment)
        {
            this._authService = auhtService;
            this._webHostEnvironment = webHostEnvironment;
        }

      

        public IActionResult Account()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(AdminAccountRegistraion model)
        {
            if (!ModelState.IsValid) { return RedirectToAction(nameof(Account)); }
            if (model.ProfilePicture != null)
            {
                string folder = "adminAccount/cover/";
                folder+=Guid.NewGuid().ToString()+"_"+ model.ProfilePicture.FileName;
                model.PictureUrl = "/"+folder;
                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                await model.ProfilePicture.CopyToAsync(new FileStream(serverFolder,FileMode.Create));
            }
            model.Role = "Admin";
            var result = await this._authService.RegisterAdminAsync(model);
            TempData["msg"] = result.Message;
            return RedirectToAction(nameof(Account));

        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await this._authService.LogoutAsync();
            return RedirectToAction("Index","Home",new {area=""});
        }
    }
}
