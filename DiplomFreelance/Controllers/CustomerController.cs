using DiplomFreelance.Controllers.BusinessLogic;
using DiplomFreelance.Controllers.BusinessLogic.IService;
using DiplomFreelance.Models;
using DiplomFreelance.Models.FreelanceModels;
using DiplomFreelance.Models.FreelanceModels.Convertors;
using DiplomFreelance.Models.FreelanceModels.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiplomFreelance.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["FreelanceConnection"].ConnectionString;
       
        private static IServiceExecutor _serviceExecutor;
        private static IServiceOrder _serviceOrder;
        private static IServiceOffer _serviceOffer;
        private static IServiceSubcategory _serviceSubcategory;
        private static IServiceCategory _serviceCategory;
        private static IServiceService _serviceService;
        private static IServiceCustomer _serviceCustomer;
        private static IServiceFileOrder _serviceFileOrder;
        public CustomerController(IServiceResponse serviceResponse,
            IServiceExecutor serviceExecutor,
            IServiceCustomer serviceCustomer,
            IServiceOrder serviceOrder,
            IServiceSubcategory serviceSubcategory,
            IServiceCategory serviceCategory,
            IServiceOffer serviceOffer,
            IServiceService serviceService, IServiceFileOrder serviceFileOrder)
        {
            _serviceCategory = serviceCategory;
            _serviceExecutor = serviceExecutor;
            _serviceOrder = serviceOrder;
            _serviceSubcategory = serviceSubcategory;
            _serviceService = serviceService;
            _serviceCustomer = serviceCustomer;
            _serviceOffer = serviceOffer;
            _serviceFileOrder = serviceFileOrder;
        }

        //Метод, который проверяет забанен ли заказчик
        private void CheckStatusCustomer()
        {
            if (_serviceCustomer.GetCustomerByUserID(User.Identity.GetUserId()).IsBanned)
            {
                HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            }
        }

        //Метод, служащий для вывода категорий
        public ActionResult _OrderCategoryPartial(int? idCategory)
        {
            CheckStatusCustomer();

            if (idCategory != null)
            {
                var domain = _serviceSubcategory.GetSubcategories((int)idCategory);
                var list = SubcategoryConvertor.ConvertToSubcategoryViewModel(domain);
                return PartialView("_OrderSubcategoryPartial", list);
            }
            else
            {
                var list = CategoryConvertor.ConvertToCategoryViewModel(_serviceCategory.GetCategories());

                return View(list);
            }
        }


        //Метод контроллера, который управляет отоблажением страницы создания заказа
        [HttpGet]
        public ActionResult Order(string idexec)
        {
            CheckStatusCustomer();

            if (String.IsNullOrEmpty(idexec))
            {
                var categorys = _serviceCategory.GetCategories();

                return View(new CreateOrderViewModel { categoryViewModels = categorys.ConvertToCategoryViewModel(), executorViewModel = null });
            }
            else
            {
                var categorys = _serviceCategory.GetCategories();
                var executor = _serviceExecutor.GetExecutorByIdUser(idexec);

                return View(new CreateOrderViewModel { categoryViewModels = categorys.ConvertToCategoryViewModel(), executorViewModel = executor.ConvertToExecutorViewModel() });
            }
        }

        //Метод, принимающий данные с формы для создания заказа
        [HttpPost]
        public ActionResult Order(CreateOrderViewModel model)
        {
            CheckStatusCustomer();

            if (User.Identity.IsAuthenticated)
            {

                var categorys = _serviceCategory.GetCategories();
                model.categoryViewModels = categorys.ConvertToCategoryViewModel();

                if (!ModelState.IsValid) return View(model);

                model.ID = Guid.NewGuid();

                if (!_serviceOrder.CreateNewOrder(model, User.Identity.GetUserId()))
                {
                    return RedirectToAction("Catalog", "Global", new { /*result = new Result {*/ message = "Ваш заказ не был опубликован!"/*, success = false }*/ });
                } 
                if (!String.IsNullOrEmpty(model.ID_Executor))
                {
                    _serviceOffer.CreateNewOffer(new OfferViewModel() { ID_Executor = model.ID_Executor, ID_Order =  model.ID});
                }


                //List<string> URLs = new List<string>();
                foreach (var img in model.Photo)
                {
                    if (img != null)
                    {
                        using (Image image = Image.FromStream(img.InputStream))
                        {
                            string nameFile = "";
                            if (img.FileName.LastIndexOf('\\') != -1)
                            {
                                nameFile = img.FileName.Substring(img.FileName.LastIndexOf('\\'), img.FileName.Length - img.FileName.LastIndexOf('\\'));
                            }
                            else
                            {
                                nameFile = img.FileName;
                            }
                            FileOrderViewModel fileOrder = new FileOrderViewModel
                            {
                                ID_Order = model.ID,
                                Image = ImageService.LoadImg(image),
                                Name = nameFile
                            };
                            _serviceFileOrder.CreateNewFileOrder(fileOrder);

                        }
                    }
                }
                return RedirectToAction("Catalog", "Global", new { /*result = new Result {*/ message = "Ваш заказ был опубликован!"/*, success = true }*/ });
            }
            return RedirectToAction("Register", "Account");
        }

        //Метод контроллера, который выводит все заказы заказчика, относительно входных параметров
        public ActionResult MyOrders(string message, int? idStatus, bool? isBanned)
       {
            CheckStatusCustomer();

            if (!String.IsNullOrEmpty(message))
            {
                ViewBag.Result = message;
            }
            var customer_id = _serviceCustomer.GetCustomerByUserID(User.Identity.GetUserId());
            var orders = _serviceOrder.GetOrdersByCustomerId(customer_id.ID_User);
            ViewBag.CountBannedOrders = orders.Where(x=>x.IsBanned == true).Count();
            if (isBanned != null && isBanned == true)
            {
                orders = orders.Where(x => x.IsBanned == true).ToList();
              
                return Request.IsAjaxRequest() ?
             (ActionResult)PartialView("_MyOrdersPartial", orders.ConvertToOrderViewModel())
             : View(orders.ConvertToOrderViewModel());
            }
            if (idStatus == null || idStatus == 1)
            {
                orders = orders.Where(x => x.Status.ID == 1 && x.IsBanned == false).ToList();
            }
            else
            {
                orders = orders.Where(x => x.Status.ID == idStatus && x.IsBanned == false).ToList();
            }

            return Request.IsAjaxRequest() ?
               (ActionResult)PartialView("_MyOrdersPartial", orders.ConvertToOrderViewModel()) 
               : View(orders.ConvertToOrderViewModel());

        }

        //Метод, удалающий заказ из бд
        public ActionResult DeleteOrder(Guid idOrder)
        {
            CheckStatusCustomer();

            var order = _serviceOrder.GetOrderById(idOrder);

            if (order.Status.ID != 2 && order.IsBanned != true)
            {
                if (_serviceOrder.DeleteOrderById(idOrder))
                {
                    return RedirectToAction("MyOrders", "Customer", new { message = "Ваш заказ был удален!" });
                }
                else
                {
                    return RedirectToAction("MyOrders", "Customer", new { message = "Ваш заказ не был удален!"});
                }
            }
            else
            {
                return RedirectToAction("MyOrders", "Customer", new { message = "Ваш заказ выполняется!"});
            }

        }

        //Метод, который добавляет к заказу выбранного исполнителя
        public ActionResult SetExecutor(Guid idOrder, string idExec, decimal budget)
        {
            CheckStatusCustomer();

            var order = _serviceOrder.GetOrderById(idOrder);
            if (order.IsBanned != true)
            {
                if (_serviceOrder.SetExecutorInOrder(idOrder, idExec, budget))
                {
                    return RedirectToAction("MyOrders", "Customer", new { message = "Вы назначили исполнителя на заказ!"});
                }
                else
                {
                    return RedirectToAction("MyOrders", "Customer", new { message = "Исполнитель не был названчен!"});
                }                      
            }
            return RedirectToAction("MyOrders", "Customer", new { message = "Ваш заказ заблокирован!"});

        }

        //Метод, который меняет статус определенному заказу на завершенный
        public ActionResult CompleteOrder(Guid idOrder)
        {
            CheckStatusCustomer();

            var order = _serviceOrder.GetOrderById(idOrder);
            if (order.IsBanned != true)
            {
                if (_serviceOrder.SetOrderStatusIsComplete(idOrder))
                {
                    return RedirectToAction("MyOrders", "Customer", new { message = "Ваш заказ был завершен!"});
                }
                else
                {
                    return RedirectToAction("MyOrders", "Customer", new { message = "Ваш заказ не был завершен!"});
                }
            }
            else
            {
                return RedirectToAction("MyOrders", "Customer", new { message = "Ваш заказ заблокирован!"});
            }
          
        }

    }
}