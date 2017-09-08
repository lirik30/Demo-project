using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;

namespace MvcPL.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _service;

        public PostController(IPostService service)
        {
            _service = service;
        }

        // GET: Post
        public ActionResult Index()
        {
            var posts = _service.GetAllPostEntities().Select(p => new PostViewModel
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                Tag = p.Tag,
                CreateTime = p.CreateTime,
                UpdateTime = p.UpdateTime,
                BlogId = p.BlogId
            });
            return View(posts);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var post = _service.GetPostEntity((int)id);
            if (post == null)
                return HttpNotFound();

            return View(post.ToMvcPost());
        }

        [HttpGet]
        public ActionResult Create(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            TempData["BlogId"] = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostViewModel postViewModel)
        {
            postViewModel.BlogId = (int)TempData["BlogId"];
            postViewModel.CreateTime = DateTime.Now;
            postViewModel.UpdateTime = DateTime.Now;
            _service.CreatePost(postViewModel.ToBllPost());
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var post = _service.GetPostEntity((int)id);
            if (post == null)
                return HttpNotFound();

            return View(post.ToMvcPost());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostViewModel postViewModel)
        {
            Debug.WriteLine("____id____" + postViewModel.Id);//something wrong with datetime
            var dt = DateTime.Now;
            postViewModel.UpdateTime = dt;
            Debug.WriteLine("____dt____" + dt);
            Debug.WriteLine("____ut____" + postViewModel.UpdateTime);
            Debug.WriteLine("____ct____" + postViewModel.CreateTime);
            _service.UpdatePost(postViewModel.ToBllPost());
            return RedirectToAction("Index");
        }

    }
}