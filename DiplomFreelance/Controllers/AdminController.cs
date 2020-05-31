using DiplomFreelance.Controllers.BusinessLogic.IService;
using DiplomFreelance.Models;
using DiplomFreelance.Models.FreelanceModels;
using DiplomFreelance.Models.FreelanceModels.Convertors;
using DiplomFreelance.Models.FreelanceModels.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiplomFreelance.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        private static IServiceExecutor _serviceExecutor;
        private static IServiceOrder _serviceOrder;
        private static IServiceSubcategory _serviceSubcategory;
        private static IServiceCategory _serviceCategory;
        private static IServiceCustomer _serviceCustomer;
        public AdminController(IServiceExecutor serviceExecutor,
           IServiceOrder serviceOrder, 
           IServiceSubcategory serviceSubcategory,
           IServiceCustomer serviceCustomer,
           IServiceCategory serviceCategory)
        {
            _serviceExecutor = serviceExecutor;
            _serviceOrder = serviceOrder;
            _serviceCategory = serviceCategory;
            _serviceSubcategory = serviceSubcategory;
            _serviceCustomer = serviceCustomer;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ChooseCategory()
        {
            var categories = _serviceCategory.GetCategories();

            return View(categories.ConvertToCategoryViewModel());
        }

        public ActionResult ChooseCategoryOrder()
        {
            var categories = _serviceCategory.GetCategories();

            return View(categories.ConvertToCategoryViewModel());
        }

        #region crudCategories
        public ActionResult CategoryView()
        {
            return View(_serviceCategory.GetCategories().ConvertToCategoryViewModel());
        }
        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCategory(CategoryViewModel item)
        {
            _serviceCategory.CreateCategory(item.ConvertFromViewModelToDBModel());
            return RedirectToAction("CategoryView");
        }

        public ActionResult EditCategory(int id)
        {
            var item = _serviceCategory.GetCategoryById(id);
            return View(item.ConvertToCategoryViewModel());
        }

        [HttpPost]
        public ActionResult EditCategory(CategoryViewModel item)
        {
            _serviceCategory.UpdateCategory(item.ConvertFromViewModelToDBModel());
            return RedirectToAction("CategoryView");
        }

        public ActionResult DeleteCategory(int id)
        {
            var item = _serviceCategory.GetCategoryById(id);
            return View(item.ConvertToCategoryViewModel());
        }

        [HttpPost]
        public ActionResult DeleteCategory(CategoryViewModel item)
        {
            _serviceCategory.DeleteCategory(item.ID);
            return RedirectToAction("CategoryView");
        }
        //SUBCATEGORY
        public ActionResult SubcategoryView(int id)
        {
            var item = _serviceSubcategory.GetSubcategories(id).ConvertToSubcategoryViewModel();
            var category = _serviceCategory.GetCategoryById(id).ConvertToCategoryViewModel();
            foreach (var itemSubcat in item)
            {
                itemSubcat.Category = category;
            }
            ViewBag.Name = category.Name;

            if (item.Count() == 0)
            {
                item.Add(new SubcategoryViewModel { Name = "Подкатегорий не существует", ID = 0, Category = category });
            }
            return View(item);
        }

        public ActionResult CreateSubcategory(int id)
        {
            var category = _serviceCategory.GetCategoryById(id).ConvertToCategoryViewModel();
            ViewBag.Name = category.Name;
            return View(new SubcategoryViewModel { Category = category });
        }

        [HttpPost]
        public ActionResult CreateSubcategory(SubcategoryViewModel item)
        {
            _serviceSubcategory.CreateSubcategory(item.ConvertFromViewModelToDBModel());
            return RedirectToAction("SubcategoryView", new { id = item.Category.ID });
        }

        public ActionResult EditSubcategory(int id, int idCat)
        {
            var category = _serviceCategory.GetCategoryById(idCat).ConvertToCategoryViewModel();

            var item = _serviceSubcategory.GetSubcategoryById(id).ConvertToSubcategoryViewModel();

            item.Category = category;

            return View(item);
        }

        [HttpPost]
        public ActionResult EditSubcategory(SubcategoryViewModel item)
        {
            int idCategory = item.Category.ID;
            _serviceSubcategory.UpdateSubcategory(item.ConvertFromViewModelToDBModel());
            return RedirectToAction("SubcategoryView", new { id = idCategory });
        }

        public ActionResult DeleteSubcategory(int id, int idCat)
        {
            var category = _serviceCategory.GetCategoryById(idCat).ConvertToCategoryViewModel();

            var item = _serviceSubcategory.GetSubcategoryById(id).ConvertToSubcategoryViewModel();
            item.Category = category;
            return View(item);
        }

        [HttpPost]
        public ActionResult DeleteSubcategory(SubcategoryViewModel item)
        {
            int idCategory = item.Category.ID;
            _serviceSubcategory.DeleteSubcategory(item.ID);
            return RedirectToAction("SubcategoryView", new { id = idCategory });
        }

        #endregion

        #region bans
        public ActionResult BanOrder(Guid idOrder, bool isBanned)
        {
            if (isBanned)
                _serviceOrder.BannedOrder(idOrder);
            else
                _serviceOrder.UnBannedOrder(idOrder);

            var customer = _serviceOrder.GetOrderById(idOrder).Customer.ID_User;

            return RedirectToAction("CustomerProfile", "Admin", new { id = customer });
        }

        public ActionResult BanCustomer(string id, bool isBanned)
        {
            if (isBanned)
                _serviceCustomer.BannedCustomer(id);
            else
                _serviceCustomer.UnBannedCustomer(id);


            return RedirectToAction("CustomerForAdmin", "Admin", new {  isBanned = !isBanned });
        }
        public ActionResult BanExecutor(string idExecutor, bool isBanned, int idSubcat)
        {
            if (isBanned)
                _serviceExecutor.BannedExecutor(idExecutor);
            else
                _serviceExecutor.UnBannedExecutor(idExecutor);


            return RedirectToAction("ChooseCategory", new { idSubcat, isBanned=!isBanned });

        }
        #endregion

        #region details
        public ActionResult CustomerProfile(string id)
        {

            var customer = _serviceCustomer.GetCustomerByUserID(id).ConvertToCustomerViewModel();
            var orders = _serviceOrder.GetOrdersByCustomerId(id).ConvertToOrderViewModel();
            var viewModel = new CustomerForAdminViewModel() { Customer = customer, Orders = orders };
            return View(viewModel);
        }

        public ActionResult _OrderDetailsPartial(Guid idOrder)
        {
            var orders = _serviceOrder.GetOrderById(idOrder);
            return PartialView(orders.ConvertToOrderViewModel());
        }
        #endregion

        #region lists
        public ActionResult OrdersForAdmin(int idSubcat, bool isBanned = false, int? page = 1)
        {
            ViewBag.Subcat = _serviceSubcategory.GetSubcategoryById((int)idSubcat).Name;

            Session["idSubcat"] = idSubcat;
            Session["isBanned"] = isBanned;

            var orders = _serviceOrder.GetOrdersBySubcategoryId(idSubcat).Where(x => x.IsBanned == isBanned).ToList();

            int pageNumber = (page ?? 1);
            int pageSize = 4;

            return View(orders.ConvertToOrderViewModel().ToPagedList(pageNumber, pageSize));
        }
        public ActionResult CustomerForAdmin(bool isBanned = false, int? page = 1)
        {
            Session["isBanned"] = isBanned;

            var customers = _serviceCustomer.GetAllCustomer().Where(x => x.IsBanned == isBanned).ToList();


            int pageNumber = (page ?? 1);
            int pageSize = 4;

            return View(customers.ConvertToCustomerViewModel().ToPagedList(pageNumber, pageSize));

        }
        public ActionResult ExecutorForAdmin(int idSubcat, bool isBanned = false, int? page = 1)
        {
            List<DomainExecutor> executors = new List<DomainExecutor>();

            executors = _serviceExecutor.GetExecutorsByIdSubcategory((int)idSubcat).Where(x => x.IsBanned == isBanned).ToList();

            ViewBag.Subcat = _serviceSubcategory.GetSubcategoryById((int)idSubcat).Name;

            Session["idSubcat"] = idSubcat;
            Session["isBanned"] = isBanned;

            int pageNumber = (page ?? 1);
            int pageSize = 4;

            return View(executors.ConvertToExecutorViewModel().ToPagedList(pageNumber, pageSize));
        }

        public ActionResult CustomerListForAdmin(string email)
        {
            var list = _serviceCustomer.GetAllCustomer().Where(x=>x.IsBanned==false).ToList();
            if (!String.IsNullOrEmpty(email))
            {
                list = list.Where(x => x.Email.Contains(email)).ToList();
            }
          
            return View(list.ConvertToCustomerViewModel());

        }
        public ActionResult AdminList()
        {


            ApplicationDbContext dbIdentity = new ApplicationDbContext();
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            string roleName = "Admin";
            var role = roleManager.Roles.Single(r => r.Name == roleName);
            var users = userManager.Users.Where(u => u.Roles.Any(r => r.RoleId == role.Id));
            return View(users);

        }
        #endregion

        #region getRoles
        public ActionResult RemoveFromRoleAdmin(string idAdmin)
        {
            //

            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            userManager.RemoveFromRole(idAdmin, "Admin");
            userManager.AddToRole(idAdmin, "Customer");

            var user = userManager.Users.ToList().Find(x => x.Id == idAdmin);

            CustomerViewModel customer = new CustomerViewModel() { ID = idAdmin, Name = user.UserName, Email = user.Email };

            _serviceCustomer.CreateCustomer(customer);

            return RedirectToAction("AdminList");

        }
        public ActionResult GetRoleAdmin(string idCustomer)
        {
            //добавление пользователю роли в Identity



            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            userManager.AddToRole(idCustomer, "Admin");
            userManager.RemoveFromRole(idCustomer, "Customer");

            _serviceCustomer.DeleteCustomer(idCustomer);

            return RedirectToAction("CustomerListForAdmin");

        }

        #endregion
       

       
    }
}