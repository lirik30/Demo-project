﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BLL.Interfaces.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;

namespace MvcPL.Controllers
{
    //USERS CAN CHANGE ANOTHER'S POST
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService; //ajax запросы

        public PostController(IPostService postService, ICommentService commentService)
        {
            _postService = postService;
            _commentService = commentService;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var posts = _postService.GetAllPostEntities().Select(p => p.ToMvcPost());
            return View(posts);
        }

        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var post = _postService.GetPostEntity((int)id);
            if (post == null)
                return HttpNotFound();

            var comments = _commentService.GetAllPostEntities().Where(c => c.PostId == id).OrderBy(c => c.CreateTime).
                           Select(c => c.ToMvcComment());
            TempData["Comments"] = comments.ToList();
            Debug.WriteLine("_____________IMHERE_______");
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

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(PostViewModel postViewModel)
        {
            if(!ModelState.IsValid)
                return View();
            postViewModel.BlogId = (int)TempData["BlogId"];
            postViewModel.CreateTime = DateTime.Now;
            postViewModel.UpdateTime = DateTime.Now;
            _postService.CreatePost(postViewModel.ToBllPost());
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var post = _postService.GetPostEntity((int)id);
            if (post == null)
                return HttpNotFound();

            return View(post.ToMvcPost());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(PostViewModel postViewModel)
        {
            postViewModel.UpdateTime = DateTime.Now;
            _postService.UpdatePost(postViewModel.ToBllPost());
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var post = _postService.GetPostEntity((int)id);
            if (post == null)
                return HttpNotFound();

            return View(post.ToMvcPost());
        }


        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = _postService.GetPostEntity(id);
            _postService.DeletePost(product);
            return RedirectToAction("Index");
        }

    }
}