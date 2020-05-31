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

        //[HttpGet]
        //public ActionResult Services(int IdSubcategory, int? page = 1)
        //{
        //    var executors = _serviceExecutor.GetExecutorsByIdSubcategory(IdSubcategory).Where(x => x.IsBanned == false).ToList();
        //    var executorsViewModel = executors.ConvertToExecutorViewModel().OrderByDescending(x => x.Raiting);
        //    //foreach (var item in executors)
        //    //{
        //    //    executorsViewModel.Add(ExecutorConvertor.ConvertToExecutorViewModel(item));
        //    //}
        //    ViewBag.SubcatId = IdSubcategory;
        //    int pageSize = 2;
        //    int pageNumber = (page ?? 1);

        //    return View(executorsViewModel.ToPagedList(pageNumber, pageSize));
        //}

        public ActionResult RemoveFilters()
        {
            Session["city"] = null;
            Session["price"] = null;
            Session["place"] = null;
            Session["measure"] = null;

            return RedirectToAction("Services", "Global", new { nameService = Session["SubcatName"], IdSubcategory = Session["SubcatId"] });
        }

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
                executors = _serviceExecutor.GetExecutorsForServicesViewByNameSubcategory(nameService);
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

        //[HttpGet]
        //public ActionResult Services(int IdSubcategory, int? page = 1)
        //{
        //    var executors = _serviceExecutor.GetExecutorsByIdSubcategory(IdSubcategory).Where(x=>x.IsBanned == false).ToList();
        //    var executorsViewModel = executors.ConvertToExecutorViewModel().OrderByDescending(x => x.Raiting);
        //    //foreach (var item in executors)
        //    //{
        //    //    executorsViewModel.Add(ExecutorConvertor.ConvertToExecutorViewModel(item));
        //    //}
        //    ViewBag.SubcatId = IdSubcategory;
        //    int pageSize = 2;
        //    int pageNumber = (page ?? 1);

        //    return View(executorsViewModel.ToPagedList(pageNumber, pageSize));
        //}

        //[HttpPost]
        //public ActionResult Services(string nameService,int? IdSubcategory, string city, decimal? price, int? place, int? measure)
        //{
        //    IEnumerable<DomainExecutor> executors = new List<DomainExecutor>();
        //    if (IdSubcategory != null)
        //    {
        //        executors = _serviceExecutor.GetExecutorsByIdSubcategory((int)IdSubcategory);
        //    }
        //    else
        //    {
        //        executors = _serviceExecutor.GetExecutorsForServicesViewByNameSubcategory(nameService);
        //    }
        //    if (!String.IsNullOrEmpty(city))
        //    {
        //        executors = executors.Where(x => x.City == city).ToList();
        //    }
        //    if (price != null)
        //    {              
        //        executors = executors.Where(x => x.Services.Any(y => y.Price <= price));
        //    }
        //    if (place != null && place != 0)
        //    {              
        //        executors = executors.Where(x => x.Services.Any(y => y.Place.ID == place));

        //    }
        //    if (measure != null && measure != 0)
        //    {             
        //        executors = executors.Where(x => x.Services.Any(y => y.Measure.ID == measure));
        //    }
        //    ViewBag.SubcatName = nameService;
        //    ViewBag.SubcatId = IdSubcategory;

        //    int pageSize = 2;
        //    int pageNumber = 1;

        //    return Request.IsAjaxRequest()
        //      ? (ActionResult)PartialView("_ServicesPartial", executors.Where(x => x.IsBanned == false).ToList().ConvertToExecutorViewModel().OrderByDescending(x => x.Raiting).ToPagedList(pageNumber, pageSize))
        //      : View(executors.Where(x => x.IsBanned == false).ToList().ConvertToExecutorViewModel().OrderByDescending(x => x.Raiting).ToPagedList(pageNumber, pageSize));
        //}

        public ActionResult _ServiceDetailsPartial(int idservice)
        {
            var service = _serviceService.GetServicesByID(idservice);
            return PartialView(service.ConvertToServiceViewModel());
        }

        public ActionResult _ChatPartial()
        {
            return PartialView();
        }

        public ActionResult Support()
        {

            return View();
        }

        public ActionResult Executor(string id)
        {
            var executor = _serviceExecutor.GetExecutorByIdUser(id);

            return View(executor.ConvertToExecutorViewModel());

        }
    }
}