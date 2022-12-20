using Dotnet6MvcLogin.Models;
using FitPortal.Areas.Admin.Models;
using FitPortal.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FitPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PostController : Controller
    {
        private readonly DatabaseContext dbcon;
        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly IHttpContextAccessor _contextAccessor;
        public PostController(DatabaseContext dbcon, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor contextAccessor)
        {
            this.dbcon = dbcon;
            this._webHostEnvironment = webHostEnvironment;
            this._contextAccessor = contextAccessor;
        }
        public async Task<IActionResult> ManagePost()
        {
            var inforPost  = await (from post in dbcon.Posts
                              join category in dbcon.Categories on post.CategoryID equals category.Id
                              join user in dbcon.Users on post.UserID equals user.Id
                              select new
                              {
                                  Id = post.Id,
                                  PostName = post.PostName,
                                  CategoryName = category.CategoryName,
                                  DateCreate = post.DateCreated,
                                  IsDisplay = post.IsDisplay,
                                  UserCreate = user.Name
                              }).ToListAsync();
            List<PostInformationViewModel> infor = new List<PostInformationViewModel>();
            foreach(var item in inforPost)
            {
                PostInformationViewModel viewModel = new PostInformationViewModel();
                viewModel.Id = item.Id;
                viewModel.PostName = item.PostName;
                viewModel.CategoryName = item.CategoryName;
                viewModel.UserCreate = item.UserCreate;
                viewModel.DateCreated = item.DateCreate;
                viewModel.IsDisplay=item.IsDisplay;
                infor.Add(viewModel);
            }
            ViewBag.listInfor = infor;
            return View();
        }
        public IActionResult AddPost()
        {
            List<PostCategory> listCategory = dbcon.Categories.ToList();
            SelectList selectListItems = new SelectList(listCategory, "Id", "CategoryName");
            ViewBag.CategoryList = selectListItems;
            return View();
        }
        public IActionResult ManageCategory() 
        { 
            List<PostCategory> listCategory = dbcon.Categories.ToList();
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
                await dbcon.Categories.AddAsync(input);
                await dbcon.SaveChangesAsync();
            }
            return RedirectToAction("ManageCategory", "Post");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPost(PostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var post = new PostInfor();
                if(model.Picture != null)
                {
                    string folder = "post/cover/";
                    folder += Guid.NewGuid().ToString() + "_" + model.Picture.FileName;
                    post.Picture = "/" + folder;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    await model.Picture.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                }
                else
                {
                    return RedirectToAction("AddPost", "Post");
                }
                string userID = _contextAccessor.HttpContext.Session.GetString("user_id");
                post.UserID = userID;
                post.PostName = model.PostName;
                post.DateCreated = model.DateCreated;
                post.CategoryID = model.CategoryID;
                post.Content = model.Content;
                post.IsDisplay = false;
                post.Describe = model.Describe;
                await dbcon.Posts.AddAsync(post);
                await dbcon.SaveChangesAsync();
            }
            return RedirectToAction("ManagePost","Post");
        }
        
        [HttpGet]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await dbcon.Posts.FindAsync(id);
            dbcon.Posts.Remove(post);
            await dbcon.SaveChangesAsync();
            return RedirectToAction("ManagePost", "Post");
        }
        public bool ChangeStatus(int input)
        {
            var post = dbcon.Posts.Find(input);
            post.IsDisplay = !post.IsDisplay;
            dbcon.SaveChanges();
            return post.IsDisplay;
        }
        [HttpPost]
        public JsonResult changeDisplay(int id)
        {
            /*
             * string title = Tbl_POST.post_title;
            Tbl_POST.status = state;
            string prefix = state == true ? "Đăng" : "Hủy đăng";
            db.Commit();
            return Json(new { Message = prefix + " \"" + title + "\" thành công" }, JsonRequestBehavior.AllowGet)
             */
            var post = dbcon.Posts.Find(id);
            post.IsDisplay = !post.IsDisplay;
            string title = post.PostName;
            string prefix;
            if (post.IsDisplay == true)
            {
                prefix = "Đăng";
            }
            else { prefix = "Hủy đăng"; }
            dbcon.SaveChanges();
            //var result = this.ChangeStatus(id);
            return Json((new { rpMessage = prefix + " \"" + title + "\" thành công" }));
        }
        [HttpPost]
        public async Task<JsonResult> UploadImage([FromForm] IFormFile upload)
        {
            if (upload.Length <= 0) return null;

            //1)check if the file is image

            //2)check if the file is too large

            //etc

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();

            //save file under wwwroot/CKEditorImages folder

            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/Images/post",
                fileName);

            using (var stream = System.IO.File.Create(filePath))
            {
                await upload.CopyToAsync(stream);
            }

            var url = $"{"/Images/post/"}{fileName}";

            var success = new uploadImage
            {
                Uploaded = 1,
                FileName = fileName,
                Url = url
            };

            return new JsonResult(success);
        }
    }
}
