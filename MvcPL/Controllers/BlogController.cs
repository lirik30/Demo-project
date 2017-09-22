using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BLL.Interfaces.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;

namespace MvcPL.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _service;

        public BlogController(IBlogService service)
        {
            _service = service;
        }


        public ActionResult Index()
        {
            var blogs = _service.GetAllBlogEntities().Select(blog => blog.ToMvcBlog());
            return View(blogs);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var blog = _service.GetBlogEntity((int)id);
            if (blog == null)
                return HttpNotFound();

            return View(blog.ToMvcBlog());
        }

        [HttpGet]
        public ActionResult Create(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var blog = _service.GetBlogEntity((int) id);
            if (blog != null)
                return View("BlogExistsError");

            TempData["UserId"] = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BlogViewModel blogViewModel)
        {
            if (!ModelState.IsValid)
                return View(); 
            blogViewModel.UserId = (int)TempData["UserId"];
            blogViewModel.CreateTime = DateTime.Now;
            _service.CreateBlog(blogViewModel.ToBllBlog());
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var blog = _service.GetBlogEntity((int)id);
            if (blog == null)
                return HttpNotFound();

            return View(blog.ToMvcBlog());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BlogViewModel blogViewModel)
        {
            if (!ModelState.IsValid)
                return View(blogViewModel);
            _service.UpdateBlog(blogViewModel.ToBllBlog());
            return RedirectToAction("Index");
        }

        
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var blog = _service.GetBlogEntity((int)id);
            if (blog == null)
                return HttpNotFound();

            return View(blog.ToMvcBlog());
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = _service.GetBlogEntity(id);
            _service.DeleteBlog(product);
            return RedirectToAction("Index");
        }
    }
}