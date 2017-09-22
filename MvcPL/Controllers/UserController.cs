using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;

namespace MvcPL.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        private string GetMD5Hash(string input)
        {
            var md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            var sBuilder = new StringBuilder();
            foreach (var b in data)
                sBuilder.Append(b.ToString("x2"));
            return sBuilder.ToString();
        }

        public ActionResult GetImage(int id)
        {
            var image = _service.GetUserEntity(id)?.Image;
            if (image == null)
                return File(Server.MapPath("~/App_Data/NoAvatar.png"), "image/png");
            return File(image, "image/jpeg");
        }

        public ActionResult Index()
        {
            var users = _service.GetAllUserEntities().Select(user => user.ToMvcUser());
            return View(users);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel userViewModel, HttpPostedFileBase[] uploadImage)
        {
            if (!ModelState.IsValid)
                return View();
            if (uploadImage[0] != null)
            {
                byte[] imageData;
                using (var binaryReader = new BinaryReader(uploadImage[0].InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage[0].ContentLength);
                }

                userViewModel.Image = imageData;
            }

            userViewModel.Password = GetMD5Hash(userViewModel.Password);//we get response with the open password...?
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
        public ActionResult Edit(UserViewModel userViewModel, HttpPostedFileBase[] uploadImage)
        {
            if (!ModelState.IsValid)
                return View(userViewModel);
            if (uploadImage[0] != null)
            {
                byte[] imageData;
                using (var binaryReader = new BinaryReader(uploadImage[0].InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage[0].ContentLength);
                }

                userViewModel.Image = imageData;
            }

            userViewModel.Password = GetMD5Hash(userViewModel.Password);
            _service.UpdateUser(userViewModel.ToBllUser());
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var user = _service.GetUserEntity((int)id);
            if (user == null)
                return HttpNotFound();

            return View(user.ToMvcUser());
        }


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