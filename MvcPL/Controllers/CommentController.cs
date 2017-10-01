using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
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

        public ActionResult Index(int? id)
        {
            if(id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var comments = _commentService.GetAllPostEntities().Where(c => c.PostId == id)
                .Select(c => c.ToMvcComment());

            return View(comments);
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(CommentViewModel commentViewModel)
        {
            commentViewModel.PostId = (int)TempData.Peek("PostId");
            var o = TempData.Peek("UserId");
            int id;
            if(!Int32.TryParse(o.ToString(), out id))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            commentViewModel.UserId = id;
            commentViewModel.CreateTime = DateTime.Now;

            _commentService.CreateComment(commentViewModel.ToBllComment());

            return PartialView("Comment", commentViewModel);
        }
    }
}