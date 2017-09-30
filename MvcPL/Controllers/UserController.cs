using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Interfaces.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;

namespace MvcPL.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }
        
        [AllowAnonymous]
        public ActionResult GetImage(int id)//!!!!!!!!
        {
            var image = _service.GetUserEntity(id)?.Image;
            if (image == null)
                return File(Server.MapPath("~/App_Data/NoAvatar.png"), "image/png");
            return File(image, "image/jpeg");
        }
        
        [AllowAnonymous]
        public ActionResult Index()
        {
            var users = _service.GetAllUserEntities().Select(user => user.ToMvcUser());
            return View(users);
        }

        
        [HttpGet, Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        //Only admin can use this function
        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "Administrator")]
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
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var user = _service.GetUserEntity((int)id);

            if (user == null)
                return HttpNotFound();

            if (!HasAccess(user.Login))
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

            return View(user.ToMvcUser());
        }

        [HttpPost, ValidateAntiForgeryToken]
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
            
            _service.UpdateUser(userViewModel.ToBllUser());
            FormsAuthentication.SetAuthCookie(userViewModel.Login, false);//проверить без изменения логина
            return RedirectToAction("Details", new { id = userViewModel.Id});
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var user = _service.GetUserEntity((int)id);
            if (user == null)
                return HttpNotFound();

            if (!HasAccess(user.Login))
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

            return View(user.ToMvcUser());
        }


        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = _service.GetUserEntity(id);

            if(!HasAccess(user.Login))
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

            if (User.IsInRole("Administrator"))
            {
                _service.DeleteUser(user);
                return RedirectToAction("Index");
            }
            _service.DeleteUser(user);
            return RedirectToAction("Logoff", "Account");
        }

        [HttpGet, AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var product = _service.GetUserEntity((int)id);
            if (product == null)
                return HttpNotFound();

            return View(product.ToMvcUser());
        }

        #region ChildActions
        [ChildActionOnly, AllowAnonymous]
        public int? GetIdByLogin(string login)
        {
            return _service.GetIdByLogin(login);
        }

        [ChildActionOnly, AllowAnonymous]
        public string GetLoginById(int id)
        {
            return _service.GetLoginById(id);
        }
        #endregion

        private bool HasAccess(string login)
        {
            return login == User.Identity.Name || User.IsInRole("Administrator");
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
    }
}