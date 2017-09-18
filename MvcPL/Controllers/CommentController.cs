using System;
using System.Web.Mvc;
using BLL.Interfaces.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;

namespace MvcPL.Controllers
{
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
            commentViewModel.UserId = (int)TempData["UserId"];
            commentViewModel.CreateTime = DateTime.Now;
            _commentService.CreateComment(commentViewModel.ToBllComment());
            return RedirectToAction("Details", "Post", new {id = commentViewModel.PostId});
        }
    }
}