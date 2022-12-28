using FitPortal.Areas.Admin.HtmlHelper;
using FitPortal.Areas.Admin.Models;
using FitPortal.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FitPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DatabaseContext _dbcon;
        private readonly UserManager<ApplicationUser> _userManager;
        public RoleController(RoleManager<IdentityRole> roleManager, DatabaseContext dbcon, UserManager<ApplicationUser> userManager)
        {
            this._roleManager = roleManager;
            this._dbcon = dbcon;
            this._userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> ViewAll()
        {
            using (_roleManager)
            {
                var model = await _roleManager.Roles.ToListAsync();
                return View(model);
            }
            
        }
        [HttpGet]
        public async Task<IActionResult> UserRole()
        {
            List<UserRoleViewModel> model = new List<UserRoleViewModel>();
            var user = await _userManager.Users.ToListAsync();
            if (user != null)
            {
                foreach (var item in user)
                {
                    UserRoleViewModel itemModel = new UserRoleViewModel()
                    {
                        UserID = item.Id,
                        UserName = item.UserName,
                        Email = item.Email
                    };
                    model.Add(itemModel);
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> ManagerRole(string IDUser)
        {
            List<ManagerRoleViewModel> model = new List<ManagerRoleViewModel>();
            using (_dbcon)
            {
                var userRole = (from ur in _dbcon.UserRoles
                                where ur.UserId == IDUser
                                select ur).ToList();
                if (userRole != null)
                {
                    foreach (var item in userRole)
                    {
                            var role = await _roleManager.FindByIdAsync(item.RoleId);
                            ManagerRoleViewModel itemModel = new ManagerRoleViewModel()
                            {
                                RoleID = role.Id,
                                RoleName = role.Name
                            };
                            model.Add(itemModel);
                    }
                }
                ViewBag.UserID = IDUser;
                var userName = await _userManager.FindByIdAsync(IDUser);
                ViewBag.UserName = userName.UserName;
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> AddRoleToUser(string ID)
        {
            ViewBag.UserID = ID;
            using (_roleManager)
            {
                var role = await _roleManager.Roles.ToListAsync();
                SelectList selectListItems = new SelectList(role, "Name", "Name");
                ViewBag.Role = selectListItems;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRoleToUser(AddRoleToUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserID);
            if (user != null)
            {
                var roleInfo = await _roleManager.FindByNameAsync(model.RoleName);
                    var check = await (from ur in _dbcon.UserRoles
                                        where ur.UserId == model.UserID && ur.RoleId == roleInfo.Id
                                        select ur).FirstOrDefaultAsync();
                    if (check != null)
                    {
                        _dbcon.UserRoles.Remove(check);
                        _dbcon.SaveChanges();
                    }
                try
                {
                    var result = await _userManager.AddToRoleAsync(user, model.RoleName);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("UserRole", "Role");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return RedirectToAction("UserRole", "Role");
                }
            }
            return RedirectToAction("ManagerRole", "Role", new { IDUser = model.UserID });
        }
        [HttpGet]
        public async Task<IActionResult> DeleteRoleFromUser(string IDUser, string RoleName)
        {
            using (_userManager)
            {
                var user = await _userManager.FindByIdAsync(IDUser);
                if (user != null)
                {
                    await _userManager.RemoveFromRoleAsync(user, RoleName);
                }
            }
            return RedirectToAction("ManagerRole", "Role", new { IDUser = IDUser });
        }
        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string IDRole)
        {
            using (_roleManager)
            {
                var role = await _roleManager.FindByIdAsync(IDRole);
                RoleViewModel model = new RoleViewModel();
                model.Id = role.Id;
                model.RoleName = role.Name;
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeleteRole(string IDRole)
        {
            using (_dbcon)
            {
                var userRoleForThisRole = await _dbcon.UserRoles.Where(x => x.RoleId == IDRole).CountAsync();
                if (userRoleForThisRole > 0)
                {
                    return RedirectToAction("ViewAll", "Role");
                }
                using (_roleManager)
                {
                    var role = await _roleManager.FindByIdAsync(IDRole);
                    _roleManager.DeleteAsync(role);
                    return RedirectToAction("ViewAll", "Role");
                }
                
            } 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (_roleManager)
                {
                    try
                    {
                        var result = await _roleManager.CreateAsync(new IdentityRole() { Name = model.RoleName });
                        if (result.Succeeded)
                        {
                            return RedirectToAction("ViewAll", "Role");
                        }
                    }catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return RedirectToAction("ViewAll", "Role");
                    }
                    
                }
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddRole", model) });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (_roleManager)
                {
                    var role = await _roleManager.FindByIdAsync(model.Id);
                    if (role == null)
                    {
                        ModelState.AddModelError("", "Không tìm thấy role");
                    }
                    else
                    {
                        role.Name = model.RoleName;
                        role.NormalizedName = model.RoleName.ToUpper();
                        var result = await _roleManager.UpdateAsync(role);
                        if (result.Succeeded == false)
                        {
                            return Json(new { isValue = false, Message = "Cập nhật thất bại" });
                        }
                    }
                }
            }
            return RedirectToAction("ViewAll", "Role");
        }
    }
}
