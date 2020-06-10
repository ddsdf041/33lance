using DiplomFreelance.Controllers.BusinessLogic;
using DiplomFreelance.Controllers.BusinessLogic.IService;
using DiplomFreelance.Models.FreelanceModels;
using DiplomFreelance.Models.FreelanceModels.Convertors;
using DiplomFreelance.Models.FreelanceModels.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace DiplomFreelance.Controllers
{
    public class GlobalController : Controller
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["FreelanceConnection"].ConnectionString;
     
        private static IServiceExecutor _serviceExecutor;
        private static IServiceSubcategory _serviceSubcategory;
        private static IServiceCategory _serviceCategory;
        private static IServiceService _serviceService;

        public GlobalController(IServiceExecutor serviceExecutor,
                                IServiceSubcategory serviceSubcategory,
                                IServiceCategory serviceCategory,
                                IServiceService serviceService)
        {
            _serviceCategory = serviceCategory;
            _serviceExecutor = serviceExecutor;
            _serviceSubcategory = serviceSubcategory;
            _serviceService = serviceService;
        }

        //Метод контроллера, управляющий открытием страницы, возвращает на страницу каталог категории и подкатегории
        public ActionResult Catalog(int? idCategory, string message)
        {
            var list = _serviceCategory.GetCategories();
            if (!String.IsNullOrEmpty(message))
            {
                ViewBag.Message = message;
            }
            if (idCategory != null)
            {
                return PartialView("_SubcategoryPartial", list.Find(x => x.ID == idCategory).Subcategories.ConvertToSubcategoryViewModel());
            }
            else
            {
                return View(list.ConvertToCategoryViewModel());
            }

        }

        //метод, удалающий фильтры, сохраненные в сессии
        public ActionResult RemoveFilters()
        {
            Session["city"] = null;
            Session["price"] = null;
            Session["place"] = null;
            Session["measure"] = null;

            return RedirectToAction("Services", "Global", new { nameService = Session["SubcatName"], IdSubcategory = Session["SubcatId"] });
        }

        //Метод, управляющий отображением страницы услуг, выводит на нее исполнителей, также фильтрует их по входящим параметрам
        [HttpGet]
        public ActionResult Services(string nameService, int? IdSubcategory, string city, decimal? price, int? place, int? measure, int? page = 1)
        {
            Session["city"] = city;        
            Session["price"] = price;
            Session["place"] = place;
            Session["measure"] = measure;
            Session["SubcatName"] = nameService;
            Session["SubcatId"] = IdSubcategory;

            IEnumerable<DomainExecutor> executors = new List<DomainExecutor>();
            if (IdSubcategory != null)
            {
                executors = _serviceExecutor.GetExecutorsByIdSubcategory((int)IdSubcategory);
            }
            else
            {
                executors = _serviceExecutor.GetExecutorsForServicesViewByServiceName(nameService);
            }
            if (!String.IsNullOrEmpty(city))
            {
                executors = executors.Where(x => x.City == city).ToList();
            }
            if (price != null)
            {
                executors = executors.Where(x => x.Services.Any(y => y.Price <= price));
            }
            if (place != null && place != 0)
            {
                executors = executors.Where(x => x.Services.Any(y => y.Place.ID == place));

            }
            if (measure != null && measure != 0)
            {
                executors = executors.Where(x => x.Services.Any(y => y.Measure.ID == measure));
            }
            //ViewBag.SubcatName = nameService;
            //ViewBag.SubcatId = IdSubcategory;

            int pageNumber = (page ?? 1);
            int pageSize = 6;
         

            return Request.IsAjaxRequest()
              ? (ActionResult)PartialView("_ServicesPartial", executors.Where(x => x.IsBanned == false).ToList().ConvertToExecutorViewModel().OrderByDescending(x => x.Raiting).ToPagedList(pageNumber, pageSize))
              : View(executors.Where(x => x.IsBanned == false).ToList().ConvertToExecutorViewModel().OrderByDescending(x => x.Raiting).ToPagedList(pageNumber, pageSize));
        }
      
        //Метод частичного представления, выводящий подробную информацию об услуге
        public ActionResult _ServiceDetailsPartial(int idservice)
        {
            var service = _serviceService.GetServicesByID(idservice);
            return PartialView(service.ConvertToServiceViewModel());
        }

        //Метод, управляющий отображением страницы поддержки
        public ActionResult Support()
        {

            return View();
        }

        //Метод, принимающий данные с формы для последующей отправки их на почту
        [HttpPost]
        public ActionResult Support(SupportViewModel model)
        {
            if (ModelState.IsValid)
            {
                string message = "Email - " + model.Email
                    + "<br>Имя - " + model.Name                
                      + "<br>Телефон - " + model.Phone
                       + "<br>Сообщение - " + model.Message;
                MailAddress from = new MailAddress("33lance@mail.ru", "Сообщение от сайта");
                MailAddress to = new MailAddress("33lance@mail.ru");
                MailMessage m = new MailMessage(from, to);
                m.Subject = model.Theme;
                m.Body = message;
                m.IsBodyHtml = true;
                // адрес smtp-сервера и порт, с которого отправляется письмо
                SmtpClient smtp = new SmtpClient("smtp.mail.ru", 25);
                smtp.Credentials = new NetworkCredential("33lance@mail.ru", "frq-TsT-j8x-TZ4");
                smtp.EnableSsl = true;
                smtp.Send(m);

                return RedirectToAction("Catalog", "Global", new { message = "Ваше сообщение отправлено в поддержку!"});
            }
            return View(model);

        }

        //Метод, управляющий отображением страницы исполнителя
        public ActionResult Executor(string id)
        {
            var executor = _serviceExecutor.GetExecutorByIdUser(id);

            return View(executor.ConvertToExecutorViewModel());

        }
    }
}