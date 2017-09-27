using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Interfaces.Services;
using MvcPL.Models;
using MvcPL.Providers;

namespace MvcPL.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService _service;

        public AccountController(IUserService service)
        {
            _service = service;
        }
        

        [HttpGet, AllowAnonymous]
        public ActionResult Register()
        {
            ViewBag.OldUrl = Request.UrlReferrer?.AbsolutePath;
            return View();
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel registerViewModel, string url)
        {
            var users = _service.GetAllUserEntities(); 
            if(users.FirstOrDefault(u => u.Email == registerViewModel.Email) != null)
                ModelState.AddModelError("", "User with such email already registered");//(((
            if (users.FirstOrDefault(u => u.Login == registerViewModel.Login) != null)
                ModelState.AddModelError("", "User with such login already registered");

            if (ModelState.IsValid)
            {
                var mUser = ((CustomMembershipProvider)Membership.Provider).
                    CreateUser(registerViewModel.Login, registerViewModel.Email, registerViewModel.Password);

                if (mUser == null)
                    ModelState.AddModelError("", "Registration error.");
                else
                {
                    FormsAuthentication.SetAuthCookie(registerViewModel.Login, false);
                    if(Url.IsLocalUrl(url))
                        return Redirect(url);
                    return RedirectToAction("Index", "Post");
                }
            }
            return View(registerViewModel);//?
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            ViewBag.OldUrl = Request.UrlReferrer?.AbsolutePath;
            return View();
        }

        [HttpPost]
        [AllowAnonymous, ValidateAntiForgeryToken]
        public ActionResult Login(LogonViewModel logonViewModel, string url)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(logonViewModel.Login, logonViewModel.Password))
                {
                    FormsAuthentication.SetAuthCookie(logonViewModel.Login, logonViewModel.RememberMe);
                    if (Url.IsLocalUrl(url))
                        return Redirect(url);
                    RedirectToAction("Index", "Post");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login or password");
                }
            }
            return View(logonViewModel);
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [ChildActionOnly]
        public ActionResult LoginPartial()
        {
            return PartialView("_LoginPartial");
        }
    }
}