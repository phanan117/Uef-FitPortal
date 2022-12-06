﻿using FitPortal.Areas.Admin.Models;
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
            return View();
        }
    }
}
