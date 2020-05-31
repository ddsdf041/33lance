using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using DiplomFreelance.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using DiplomFreelance.Controllers.BusinessLogic;
using DiplomFreelance.Controllers.BusinessLogic.IService;

namespace DiplomFreelance.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        static readonly string connectionString = ConfigurationManager.ConnectionStrings["FreelanceConnection"].ConnectionString;
        private static IServiceExecutor _serviceExecutor;
        private static IServiceCustomer _serviceCustomer;
        public AccountController(IServiceCustomer serviceCustomer, IServiceExecutor serviceExecutor)
        {
            _serviceExecutor = serviceExecutor;
            _serviceCustomer = serviceCustomer;
        }

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

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            SignInStatus result;


            var userCustomer = _serviceCustomer.GetCustomerByEmail(model.Email);
            if (userCustomer!=null && userCustomer.IsBanned == true)
            {
                    ModelState.AddModelError("", "Ваш аккаунт заблокирован за нарушение правил.");
                    return View(model);
            }
            else
            {
                var userExecutor = _serviceExecutor.GetExecutorByEmail(model.Email);
                if (userExecutor != null && userExecutor.IsBanned == true)
                {
                    ModelState.AddModelError("", "Ваш аккаунт заблокирован за нарушение правил.");
                    return View(model);
                }
            }
            
            // Сбои при входе не приводят к блокированию учетной записи
            // Чтобы ошибки при вводе пароля инициировали блокирование учетной записи, замените на shouldLockout: true
            result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);

           
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Неудачная попытка входа.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterExecutor(ExecutorRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, "Executor");

                    if (model.PhotoFile != null)
                    {
                        Image image = Image.FromStream(model.PhotoFile.InputStream);
                        model.Photo = ImageService.LoadImg(image);
                    }
                    else
                    {
                        model.Photo = "https://i.imgur.com/zqlYxjX.png";
                    }
                    //Image image = Image.FromFile(@"C:\Users\kolom\Desktop\Диплом основной сюжа\DiplomFreelance\Images\no_img.png");
                    //System.IO.MemoryStream memoryStream = new MemoryStream();
                    //image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                    //byte[] b = memoryStream.ToArray();

                    var u = User.Identity.GetUserId();
                    using (SqlConnection _db = new SqlConnection(connectionString))
                    {
                        _db.Open();
                            try
                            {
                                string sqlExp = $@"INSERT INTO Executor(ID_User, Name, Email, Speciality, Telephone, Raiting, Description, Photo, City, IsBanned)
                                                   VALUES(
                                                          N'{user.Id}', 
                                                          N'{model.Name}',
                                                          N'{model.Email}',
                                                          N'{model.Speciality}',
                                                          N'{model.Phone}',
                                                          0,
                                                          N'{model.Descripton}',
                                                          N'{model.Photo}',
                                                          N'{model.City}', 'false')";
                                SqlCommand command = new SqlCommand(sqlExp, _db);
                                command.ExecuteNonQuery();
                                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                            }
                            catch (Exception ex)
                            {
                                await UserManager.DeleteAsync(user);
                                return View(ex.Message);
                            }
                        _db.Close();
                    }
                    return RedirectToAction("ExecutorProfile", "Manage");
                }
                AddErrors(result);
            }
            return View( "_ExecutorRegistrationPartial", model);
        }
        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(CustomerRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {

                    await UserManager.AddToRoleAsync(user.Id, "Customer");
                     
                    using (SqlConnection _db = new SqlConnection(connectionString))
                    {
                        _db.Open();
                            try
                            {
                                string sqlExp = $@"INSERT INTO Customer(ID_User, Name, Email, IsBanned)
                                                   VALUES(N'{user.Id}', N'{model.Name}' , N'{model.Email}', 'false')";

                                SqlCommand command = new SqlCommand(sqlExp, _db);
                                command.ExecuteNonQuery();
                                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                            }
                            catch (Exception ex)
                            {
                                await UserManager.DeleteAsync(user);
                                return View(ex.Message);
                            }
                        _db.Close();
                    }

                    // Дополнительные сведения о включении подтверждения учетной записи и сброса пароля см. на странице https://go.microsoft.com/fwlink/?LinkID=320771.
                    // Отправка сообщения электронной почты с этой ссылкой
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Подтверждение учетной записи", "Подтвердите вашу учетную запись, щелкнув <a href=\"" + callbackUrl + "\">здесь</a>");

                    return RedirectToAction("Catalog", "Global");
                    }
                    AddErrors(result);
            }           
                // Появление этого сообщения означает наличие ошибки; повторное отображение формы
                return View("_CustomerRegistrationPartial", model);         
        }
    



        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Catalog", "Global");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
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

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Catalog", "Global");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}