using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BLL.Interfaces.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;

namespace MvcPL.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IUserService _userService;

        public BlogController(IBlogService blogService, IUserService userService)
        {
            _blogService = blogService;
            _userService = userService;
        }

        //public ActionResult CreatePost(int? id)
        //{
        //    if (id == null)
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        //    if(!HasAccess((int)id))
        //        return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

        //    return RedirectToAction("Create", "Post", new {id});
        //}

        [AllowAnonymous]
        public ActionResult Index()
        {
            var blogs = _blogService.GetAllBlogEntities().Select(blog => blog.ToMvcBlog());
            return View(blogs);
        }

        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var blog = _blogService.GetBlogEntity((int)id);
            if (blog == null)
                return HttpNotFound();

            return View(blog.ToMvcBlog());
        }

        [HttpGet]
        public ActionResult Create(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (!HasAccess((int)id))
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

            var blog = _blogService.GetBlogEntity((int) id);
            if (blog != null)
                return View("BlogExistsError");

            TempData["UserId"] = id;
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(BlogViewModel blogViewModel)
        {
            if (!ModelState.IsValid)
                return View(); 
            blogViewModel.UserId = (int)TempData["UserId"];
            blogViewModel.CreateTime = DateTime.Now;
            _blogService.CreateBlog(blogViewModel.ToBllBlog());
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (!HasAccess((int)id))
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

            var blog = _blogService.GetBlogEntity((int)id);
            if (blog == null)
                return HttpNotFound();

            return View(blog.ToMvcBlog());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(BlogViewModel blogViewModel)
        {
            if (!ModelState.IsValid)
                return View(blogViewModel);
            _blogService.UpdateBlog(blogViewModel.ToBllBlog());
            return RedirectToAction("Index");
        }

        
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (!HasAccess((int)id))
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

            var blog = _blogService.GetBlogEntity((int)id);
            if (blog == null)
                return HttpNotFound();

            return View(blog.ToMvcBlog());
        }


        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = _blogService.GetBlogEntity(id);
            _blogService.DeleteBlog(product);
            return RedirectToAction("Index");
        }

        #region ChildActions

        [ChildActionOnly, AllowAnonymous]
        public string GetNameById(int id)
        {
            return _blogService.GetBlogEntity(id).BlogName;
        }
        #endregion

        private bool HasAccess(int id)
        {
            return id == _userService.GetIdByLogin(User.Identity.Name) || User.IsInRole("Administrator");
        }
    }
}