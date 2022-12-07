using FitPortal.Areas.Admin.Models;
using FitPortal.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace FitPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly DatabaseContext dbcon;

        public PostController(DatabaseContext dbcon)
        {
            this.dbcon = dbcon;
        }
        public IActionResult ManagePost()
        {
            return View();
        }
        public IActionResult ManageCategory() 
        { 
            List<PostCategory> listCategory = dbcon.PostCategories.ToList();
            ViewBag.Category = listCategory;
            return View();
        }
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(PostCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var input = new PostCategory();
                input.CategoryName = model.CategoryName;
                await dbcon.PostCategories.AddAsync(input);
                await dbcon.SaveChangesAsync();
            }
            return RedirectToAction("ManageCategory", "Post");
        }
    }
}
