using FitPortal.Models;
using FitPortal.Models.Domain;
using FitPortal.Models.DTO;
using FitPortal.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitPortal.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private readonly IUserAuthenticationService _authService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ITeacherRepository _teacherRepository;
        private readonly ITeacherUserRepository _teacherUserRepository;
        public UserAuthenticationController(IUserAuthenticationService authService, IHttpContextAccessor contextAccessor, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, ITeacherRepository teacherRepository,ITeacherUserRepository teacherUserRepository)
        {
            this._authService = authService;
            this._contextAccessor = contextAccessor;
            this.userManager = userManager;
            this._signInManager = signInManager;
            this.roleManager = roleManager;
            this._teacherRepository = teacherRepository;
            this._teacherUserRepository = teacherUserRepository;
        }


        public IActionResult Login()
        {
            return View();
        }

       
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = await _authService.LoginAsync(model);
            if(result.StatusCode==1)
            {
                var user = await userManager.FindByNameAsync(model.Username);
                _contextAccessor.HttpContext.Session.SetString("user_id",user.Id);
                _contextAccessor.HttpContext.Session.SetString("user_name",user.Name);
                _contextAccessor.HttpContext.Session.SetString("user_picture",user.ProfilePicture);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Login));
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model, string? returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Action("~/");
            if (ModelState.IsValid)
            {
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("Erorr");
                }
                var user = new ApplicationUser {
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Name = model.FullName, 
                    Email = model.Email,
                    UserName = model.Email,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    var teacher = _teacherRepository.GetAll().Where(t => t.TeacherCode == model.AuthenCode).FirstOrDefault();
                    if(teacher != null)
                    {
                        var infoUser = await userManager.FindByNameAsync(model.Email);
                        TeacherUser teacherUser = new TeacherUser { TeacherID = teacher.Id, UserID = infoUser.Id };
                        var resultCreate = _teacherUserRepository.Create(teacherUser);
                        if(resultCreate == true)
                        {
                            if (!await roleManager.RoleExistsAsync("Teacher"))
                                await roleManager.CreateAsync(new IdentityRole("Teacher"));
                            if (await roleManager.RoleExistsAsync("Teacher"))
                            {
                                await userManager.AddToRoleAsync(user, "Teacher");
                            }
                        }
                        
                    }
                    result = await userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        await _signInManager.UpdateExternalAuthenticationTokensAsync(info);
                        return LocalRedirect(returnUrl);
                    }
                }
                ModelState.AddModelError("Email", "đã tồn tại!");
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            var redirect = Url.Action("ExternalLoginCallBack","UserAuthentication", new {ReturnUrl=returnUrl});
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirect);
            return Challenge(properties,provider);
        }
        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallBack(string returnurl=null, string remoteError = null)
        {
            returnurl = returnurl ?? Url.Content("~/");
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return View(nameof(Login));
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            //Sign in the user with this external login provider, if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            if (result.Succeeded)
            {
                await _signInManager.UpdateExternalAuthenticationTokensAsync(info);
                return LocalRedirect(returnurl);
            }
            else
            {
                ViewData["ReturnUrl"] = returnurl;
                ViewData["ProviderDisplayName"] = info.ProviderDisplayName;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                return View("ExternalLoginConfirmation", new ExternalLoginViewModel { Email = email });
            }
        }
        
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationModel model)
        {
            if(!ModelState.IsValid) { return View(model); }
            model.Role = "Admin";
            var result = await this._authService.RegisterAsync(model);
            TempData["msg"] = result.Message;
            return RedirectToAction(nameof(Registration));
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await this._authService.LogoutAsync();  
            return RedirectToAction(nameof(Login));
        }
        

        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult>ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
              return View(model);
            var result = await _authService.ChangePasswordAsync(model, User.Identity.Name);
            TempData["msg"] = result.Message;
            return RedirectToAction(nameof(ChangePassword));
        }

    }
}
