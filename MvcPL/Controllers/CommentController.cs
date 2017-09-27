using System;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Web.Mvc;
using BLL.Interfaces.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;

namespace MvcPL.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(CommentViewModel commentViewModel)
        {
            commentViewModel.PostId = (int)TempData["PostId"];
            var o = TempData["UserId"];
            int id;
            if(!Int32.TryParse(o.ToString(), out id))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            commentViewModel.UserId = id;
            commentViewModel.CreateTime = DateTime.Now;
            _commentService.CreateComment(commentViewModel.ToBllComment());
            return RedirectToAction("Details", "Post", new {id = commentViewModel.PostId});
        }
    }
}