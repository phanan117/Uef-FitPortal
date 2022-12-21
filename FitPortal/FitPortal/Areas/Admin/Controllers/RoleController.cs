using FitPortal.Areas.Admin.Models;
using FitPortal.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DatabaseContext _dbcon;
        public RoleController(RoleManager<IdentityRole> roleManager, DatabaseContext dbcon)
        {
            this._roleManager = roleManager;
            this._dbcon = dbcon;
        }
        [HttpGet]
        public async Task<IActionResult> ViewAll()
        {
            var model = await _roleManager.Roles.ToListAsync();
            return View(model);
        }
        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string IDRole)
        {
            var role = await _roleManager.FindByIdAsync(IDRole);
            RoleViewModel model = new RoleViewModel();
            model.Id = role.Id;
            model.RoleName = role.Name;
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteRole(string IDRole)
        {
            var userRoleForThisRole = await _dbcon.UserRoles.Where(x => x.RoleId == IDRole).CountAsync();
            if(userRoleForThisRole > 0)
            {
                return RedirectToAction("ViewAll", "Role");
            }
            var role = await _roleManager.FindByIdAsync(IDRole);
            _roleManager.DeleteAsync(role);
            return RedirectToAction("ViewAll", "Role");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole() { Name = model.RoleName });
                if (result.Succeeded)
                {
                    return RedirectToAction("ViewAll","Role");
                }
            }
            return RedirectToAction("ViewAll", "Role");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.Id);
                if(role == null)
                {
                    ModelState.AddModelError("","Không tìm thấy role");
                }
                else
                {
                    role.Name = model.RoleName;
                    role.NormalizedName = model.RoleName.ToUpper();
                    var result = await _roleManager.UpdateAsync(role);
                    if (result.Succeeded == false)
                    {
                        return Json(new {isValue = false, Message = "Cập nhật thất bại" });
                    }
                }
            }
            return RedirectToAction("ViewAll", "Role");
        }
    }
}
