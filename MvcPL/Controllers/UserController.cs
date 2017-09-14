using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BLL.Interfaces.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;

namespace MvcPL.Controllers
{
    public class UserController : Controller
    {
        private IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }


        public ActionResult Index()
        {
            var users = _service.GetAllUserEntities().Select(user => user.ToMvcUser());
            return View(users);
        }

        public ActionResult Sort()
        {
            var func = (Func<UserViewModel, object>)TempData["Sorter"];
            var users = _service.GetAllUserEntities().Select(user => user.ToMvcUser()).OrderBy(func);
            return View("Index", users);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel userViewModel)
        {
            _service.CreateUser(userViewModel.ToBllUser());
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if(id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var user = _service.GetUserEntity((int)id);
            if (user == null)
                return HttpNotFound();

            return View(user.ToMvcUser());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserViewModel userViewModel)
        {
            _service.UpdateUser(userViewModel.ToBllUser());
            return RedirectToAction("Index");
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var user = _service.GetUserEntity((int)id);
            if (user == null)
                return HttpNotFound();

            return View(user.ToMvcUser());
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = _service.GetUserEntity(id);
            _service.DeleteUser(product);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if(id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var product = _service.GetUserEntity((int) id);
            if(product == null)
                return HttpNotFound();

            return View(product.ToMvcUser());
        }
    }
}