using BotDetect.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using RoyalShop.App.App_Start;
using RoyalShop.App.Models;
using RoyalShop.Common;
using RoyalShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RoyalShop.App.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        
        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Account
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model,string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = _userManager.Find(model.UserName, model.Password);
                if (user != null)
                {
                    IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                    authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                    ClaimsIdentity identity = _userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationProperties props = new AuthenticationProperties();
                    props.IsPersistent = model.RememberMe;
                    authenticationManager.SignIn(props, identity);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        } 

        //toàn bộ phương thức sẽ chạy ở chế độ bất đồng bộ
        [HttpPost]
        [CaptchaValidation("CaptchaCode", "registerCaptcha", "Bậy zồi! lại nào! ")]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var userEmail = await _userManager.FindByEmailAsync(model.Email);
                if(userEmail != null)
                {
                    ModelState.AddModelError("email","Email đã tồn tại! chơi lại nào!");
                    return View(model);
                }
                var userByUserName = await _userManager.FindByEmailAsync(model.Email);
                if (userByUserName != null)
                {
                    ModelState.AddModelError("user", "Tài khoản đã tồn tại! chơi lại nào!");
                    return View(model);
                }
                var user = new ApplicationUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    EmailConfirmed = true,
                    BirthDay = DateTime.Now,
                    FullName = model.FullName,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address
                };

                await _userManager.CreateAsync(user, model.Password);

                var userAdmin = await _userManager.FindByEmailAsync(model.Email);
                if (userAdmin != null)
                    await _userManager.AddToRolesAsync(userAdmin.Id, new string[] { "User" });

                string content = System.IO.File.ReadAllText(Server.MapPath("/Common/Client/template/NewUser.html"));
                content = content.Replace("{{UserName}}", userAdmin.FullName);
                content = content.Replace("{{link}}", ConfigHelper.GetByKey("CurrentLink") + "dang-nhap.html");
                
                MailHelper.SendMail(userAdmin.Email, "Đăng ký thành công!", content);

                ViewData["SuccessMsg"] = "Đăng ký thành công";
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}