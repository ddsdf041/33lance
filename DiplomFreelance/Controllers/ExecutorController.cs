using DiplomFreelance.Controllers.BusinessLogic;
using DiplomFreelance.Models.FreelanceModels.ViewModel;
using DiplomFreelance.Models.FreelanceModels.Convertors;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiplomFreelance.Controllers.BusinessLogic.IService;
using PagedList;

namespace DiplomFreelance.Controllers
{
    [Authorize(Roles = "Executor")]
    public class ExecutorController : Controller
    {
        readonly static string connectionString = ConfigurationManager.ConnectionStrings["FreelanceConnection"].ConnectionString;
        private static IServiceResponse _serviceResponse;
        private static IServiceExecutor _serviceExecutor;
        private static IServiceOrder _serviceOrder;
        private static IServiceSubcategory _serviceSubcategory;
        private static IServiceCategory _serviceCategory;
        private static IServiceService _serviceService;
        private static IServicePlace _servicePlace;
        private static IServiceMeasure _serviceMeasure;

        public ExecutorController (IServiceResponse serviceResponse, 
            IServiceExecutor serviceExecutor, 
            IServiceOrder serviceOrder,
            IServiceSubcategory serviceSubcategory,
            IServiceCategory serviceCategory,
            IServiceService serviceService,
            IServicePlace servicePlace,
            IServiceMeasure serviceMeasure)
        {
            _serviceCategory = serviceCategory;
            _serviceExecutor = serviceExecutor;
            _serviceOrder = serviceOrder;
            _serviceSubcategory = serviceSubcategory;
            _serviceService = serviceService;
            _servicePlace = servicePlace;
            _serviceMeasure = serviceMeasure;
            _serviceResponse = serviceResponse;
        }

        private void CheckStatusExecutor()
        {
            if (_serviceExecutor.GetExecutorByIdUser(User.Identity.GetUserId()).IsBanned)
            {
                HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            }
        }


        [HttpGet]
        public ActionResult OrderDetails(Guid order)
        {
            CheckStatusExecutor();
            var response = _serviceResponse.GetResponseByOrderIdAndExecutorId(order, User.Identity.GetUserId());
            var model = new OrderDetailViewModel() { Order = _serviceOrder.GetOrderById(order).ConvertToOrderViewModel(), ResponseVM = response.ConvertToResponseViewModel() };
            return View(model);
        }

        [HttpPost]
        public ActionResult OrderDetails(CreateResponseViewModel model)
        {
            CheckStatusExecutor();
            model.Date = DateTime.Now;
            if (!ModelState.IsValid)
            {
                var order = new OrderDetailViewModel() { Order = _serviceOrder.GetOrderById(model.ID_Order).ConvertToOrderViewModel(), Response = model };
                
                return View(order);
            }
            if (_serviceResponse.CreateNewResponse(model, User.Identity.GetUserId()))
            {
                return RedirectToAction("ExecutorProfile", "Manage", new { message = "Вы оставили отклик" });
            }
            return RedirectToAction("ExecutorProfile", "Manage", new { message = "Не удалось оставить отклик" });
        }


        ////фильтры: новые заказы (фильтры), мои заказы, мои отклики, выполненные, на выполнении
        //[HttpGet]
        //public ActionResult OrderForExecutor() 
        //{
        //    //List<OrderViewModel> orders = new List<OrderViewModel>();
        //    //orders = _serviceOrder.GetOrderViewModelsWhereExecutorIsNull().ConvertToOrderViewModel();
        //    //orders.ca
        //    return View(_serviceOrder.GetOrderViewModelsWhereExecutorIsNull().ConvertToOrderViewModel());
        //}

        //[HttpPost]

        [HttpGet]
        public ActionResult OrderForExecutor(int idStatus = 1, string Subcategory = "", int? page = 1)
        {
            var categories = _serviceCategory.GetCategories();

            CheckStatusExecutor();

            Session["idStatus"] = idStatus;
            Session["Subcategory"] = Subcategory;


            List<OrderViewModel> orders = new List<OrderViewModel>();
            string idExec = User.Identity.GetUserId();
            switch (idStatus)
            {
                case 1: //новые
                    orders = _serviceOrder.GetOrderViewModelsWhereExecutorIsNull().ConvertToOrderViewModel();
                    break;
                case 2: //на выполнении
                    orders = _serviceOrder.GetOrdersByExecutorId(idExec).ConvertToOrderViewModel().Where(x => x.ID_Status == 2).ToList();
                    break;
                case 3: //выполненные
                    orders = _serviceOrder.GetOrdersByExecutorId(idExec).ConvertToOrderViewModel().Where(x => x.ID_Status == 3).ToList();
                    break;
                case 4: //мои отклики
                    orders = _serviceOrder.OrderWithNullExecutorAndCurrentExecutorGiveAResponse(idExec).ConvertToOrderViewModel();
                    break;
                case 5: //мне предложили
                    orders = _serviceOrder.GetOrdersOfferedToTheExecutorAndStatusIsPending(idExec).ConvertToOrderViewModel();
                    break;
            }

            if (idStatus == 1 && !String.IsNullOrEmpty(Subcategory))
            {
                orders = orders.Where(x => x.Subcategory.Contains(Subcategory)).ToList();
            }
            int pageNumber = (page ?? 1);
            int pageSize = 6;


            OrderForExecutorViewModel viewModel = new OrderForExecutorViewModel() { Categories = categories.ConvertToCategoryViewModel(), Orders = orders.ToList().ToPagedList(pageNumber, pageSize) };

            return Request.IsAjaxRequest()
          ? (ActionResult)PartialView("_OrderForExecutorPartial", viewModel.Orders)
          : View(viewModel);

            
        }

        //[HttpGet]
        //public ActionResult _OrderForExecutorPartial()
        //{





        //    //r/*eturn PartialView(orders.OrderByDescending(x => x.Date).ToList().ToPagedList(pageNumber, pageSize));*/

        //    //return Request.IsAjaxRequest()
        //    //? (ActionResult)PartialView("_OrderForExecutorPartial", orders.OrderByDescending(x => x.Date))
        //    //: PartialView(orders.OrderByDescending(x => x.Date));
        //}

        public ActionResult SetStatusForOrder(Guid idOrder, int status)
        {
            CheckStatusExecutor();
            if (status == 2)
            {
                _serviceOrder.SetExecutorInOrder(idOrder, User.Identity.GetUserId());
                return RedirectToAction("OrderForExecutor");
            }
            else if (status == 5)
            {
                _serviceOrder.SetOrderStatusIsReject(idOrder);
                return RedirectToAction("OrderForExecutor");
            }
            return RedirectToAction("ExecutorProfile", "Manage", new { message = "Не удалось отправить согласие или отказ" });
        }

        public ActionResult _ServiceCategoryPartial(int? idCategory)
        {
            CheckStatusExecutor();
            if (idCategory != null)
            {
                var list = _serviceSubcategory.GetSubcategories((int)idCategory).ConvertToSubcategoryViewModel();
                return PartialView("_ServiceSubcategoryPartial", list);
            }
            else
            {
                var list = _serviceCategory.GetCategories().ConvertToCategoryViewModel();
                return View(list);
            }
        }

        [HttpGet]
        public ActionResult CreateNewService()
        {
            CheckStatusExecutor();
            var item = new CreateServiceViewModel() { categoryViewModels = _serviceCategory.GetCategories().ConvertToCategoryViewModel() };
            
            ViewData["ID_Place"] = new SelectList(_servicePlace.GetAllPlace(), "ID", "Name");
            ViewData["ID_Measure"] = new SelectList(_serviceMeasure.GetAllMeasure(), "ID", "Name");
            return View(item);
        }

        [HttpPost]
        public ActionResult CreateNewService(CreateServiceViewModel model)
        {
            CheckStatusExecutor();
            model.categoryViewModels = _serviceCategory.GetCategories().ConvertToCategoryViewModel();

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (_serviceService.CreateNewService(model, User.Identity.GetUserId()))
            {
                return RedirectToAction("ExecutorProfile", "Manage", new { message = "Вы добавили новую услугу" });
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult UpdateService(int id)
        {
            CheckStatusExecutor();
            var item = _serviceService.GetServicesByID(id).ConvertToCreateServiceViewModel();
            item.categoryViewModels = _serviceCategory.GetCategories().ConvertToCategoryViewModel();

            ViewBag.NameCat = _serviceSubcategory.GetSubcategoryById(item.ID_Subcategory).Name;
            ViewData["ID_Place"] = new SelectList(_servicePlace.GetAllPlace(), "ID", "Name");
            ViewData["ID_Measure"] = new SelectList(_serviceMeasure.GetAllMeasure(), "ID", "Name");

            return View(item);
        }

        [HttpPost]
        public ActionResult UpdateService(CreateServiceViewModel model)
        {
            CheckStatusExecutor();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (_serviceService.UpdateService(model, User.Identity.GetUserId()))
            {
                return RedirectToAction("ExecutorProfile", "Manage", new { message = "Вы обновили услугу" });
            }
            return RedirectToAction("ExecutorProfile", "Manage", new { message = "Не удалось обновить услугу" });
        }

        public ActionResult DeleteService(int id)
        {
            CheckStatusExecutor();
            if (_serviceService.DeleteService(id))
            {
                return RedirectToAction("ExecutorProfile", "Manage", new { message = "Вы успешно удалили услугу" });
            }
            else
            {
                return RedirectToAction("ExecutorProfile", "Manage", new { message = "Не удалось удалить услугу" });
            }
        }
        public ActionResult _ServiceDetailsPartial(int id)
        {
            CheckStatusExecutor();
            return PartialView(_serviceService.GetServicesByID(id).ConvertToServiceViewModel());
        }

        public ActionResult CompleteOrder(Guid idOrder)
        {
            CheckStatusExecutor();
            if (_serviceOrder.SetOrderStatusIsComplete(idOrder))
            {
                return RedirectToAction("ExecutorProfile", "Manage", new { /*result = new Result {*/ message = "Заказ был завершен!"/*, success = true }*/ });
            }
            else
            {
                return RedirectToAction("ExecutorProfile", "Manage", new { /*result = new Result {*/ message = "Заказ не был завершен!"/*, success = true }*/ });
            }

        }

    }
}