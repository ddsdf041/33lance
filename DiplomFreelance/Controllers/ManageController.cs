using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Drawing;
using DiplomFreelance.Models;
using DiplomFreelance.Controllers.BusinessLogic;
using System.Configuration;
using DiplomFreelance.Models.FreelanceModels.Convertors;
using DiplomFreelance.Models.FreelanceModels.ViewModel;
using DiplomFreelance.Controllers.BusinessLogic.IService;

namespace DiplomFreelance.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private static IServiceExecutor _serviceExecutor;

        public ManageController()
        {
        }
        public ManageController(IServiceExecutor serviceExecutor)
        {
            _serviceExecutor = serviceExecutor;
        }
        //public ActionResult ExecutorProfile()
        //{
        //    return View(BusinessLogic.GetExecutorByUserId(User.Identity.GetUserId()));
        //}
        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        readonly string connectionString = ConfigurationManager.ConnectionStrings["FreelanceConnection"].ConnectionString;
        public ActionResult ExecutorProfile(string message)
        {
            if (!String.IsNullOrEmpty(message))
            {
                ViewBag.Message = message;
            }
            var item = _serviceExecutor.GetExecutorByIdUser(User.Identity.GetUserId()).ConvertToExecutorViewModel();
            return View(item);
        }

        [HttpGet]
        public ActionResult ChangeExecutorProfile()
        {
            return View(_serviceExecutor.GetExecutorByIdUser(User.Identity.GetUserId()).ConvertToExecutorViewModel());
        }
        [HttpPost]
        public ActionResult ChangeExecutorProfile(ExecutorViewModel changeExecutor)
        {
            
            if (changeExecutor.PhotoFile != null)
            {
                Image image = Image.FromStream(changeExecutor.PhotoFile.InputStream);
                changeExecutor.Photo = ImageService.LoadImg(image);
            }
            else
            {
                changeExecutor.Photo = _serviceExecutor.GetExecutorByIdUser(User.Identity.GetUserId()).Photo;
            }

            if (ModelState.IsValid)
            if (_serviceExecutor.UpdateExecutorDetails(changeExecutor))
                return RedirectToAction("ExecutorProfile", "Manage", new { message = "Вы успешно изменили профиль" });
            return View(changeExecutor);
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

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Ваш пароль изменен."
                : message == ManageMessageId.SetPasswordSuccess ? "Пароль задан."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Настроен поставщик двухфакторной проверки подлинности."
                : message == ManageMessageId.Error ? "Произошла ошибка."
                : message == ManageMessageId.AddPhoneSuccess ? "Ваш номер телефона добавлен."
                : message == ManageMessageId.RemovePhoneSuccess ? "Ваш номер телефона удален."
                : "";

            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };
            return View(model);
        }


        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

#region Вспомогательные приложения
        // Используется для защиты от XSRF-атак при добавлении внешних имен входа
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }


        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

#endregion
    }
}