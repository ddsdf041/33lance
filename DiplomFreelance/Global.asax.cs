using Autofac;
using Autofac.Integration.Mvc;
using DiplomFreelance.Controllers;
using DiplomFreelance.Controllers.BusinessLogic;
using DiplomFreelance.Controllers.BusinessLogic.IService;
using DiplomFreelance.Models.Repository;
using DiplomFreelance.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DiplomFreelance
{
    public static class IoCContainer
    {
        readonly static string connectionString = ConfigurationManager.ConnectionStrings["FreelanceConnection"].ConnectionString;

        public static void InitContainer(ContainerBuilder container)
        {
            container.RegisterType<ServiceCategory>().As<IServiceCategory>().SingleInstance();
            container.RegisterType<ServiceSubcategory>().As<IServiceSubcategory>().SingleInstance();
            container.RegisterType<ServiceCustomer>().As<IServiceCustomer>().SingleInstance();
            container.RegisterType<ServiceExecutor>().As<IServiceExecutor>().SingleInstance();
            container.RegisterType<ServiceFileOrder>().As<IServiceFileOrder>().SingleInstance();
            container.RegisterType<ServiceMeasure>().As<IServiceMeasure>().SingleInstance();
            container.RegisterType<ServiceOrder>().As<IServiceOrder>().SingleInstance();
            container.RegisterType<ServicePlace>().As<IServicePlace>().SingleInstance();
            container.RegisterType<ServiceResponse>().As<IServiceResponse>().SingleInstance();
            container.RegisterType<ServiceService>().As<IServiceService>().SingleInstance();
            container.RegisterType<ServiceStatus>().As<IServiceStatus>().SingleInstance();
            container.RegisterType<ServiceOffer>().As<IServiceOffer>().SingleInstance();
            container.RegisterType<ServiceChat>().As<IServiceChat>().SingleInstance();
            container.RegisterType<ServiceMessage>().As<IServiceMessage>().SingleInstance();


            container.RegisterType<CategoryRepository>().As<ICategoryRepository>().WithParameter("connectionString", connectionString).SingleInstance();
            container.RegisterType<CustomerRepository>().As<ICustomerRepository>().WithParameter("connectionString", connectionString).SingleInstance();
            container.RegisterType<ExecutorRepository>().As<IExecutorRepository>().WithParameter("connectionString", connectionString).SingleInstance();
            container.RegisterType<MeasureRepository>().As<IMeasureRepository>().WithParameter("connectionString", connectionString).SingleInstance();
            container.RegisterType<OrderRepository>().As<IOrderRepository>().WithParameter("connectionString", connectionString).SingleInstance();
            container.RegisterType<PlaceRepository>().As<IPlaceRepository>().WithParameter("connectionString", connectionString).SingleInstance();
            container.RegisterType<ResponseRepository>().As<IResponseRepository>().WithParameter("connectionString", connectionString).SingleInstance();
            container.RegisterType<ServiceRepository>().As<IServiceRepository>().WithParameter("connectionString", connectionString).SingleInstance();
            container.RegisterType<StatusRepository>().As<IStatusRepository>().WithParameter("connectionString", connectionString).SingleInstance();
            container.RegisterType<SubcategoryRepository>().As<ISubcategoryRepository>().WithParameter("connectionString", connectionString).SingleInstance();
            container.RegisterType<FileOrderRepository>().As<IFileOrderRepository>().WithParameter("connectionString", connectionString).SingleInstance();
            container.RegisterType<OfferRepository>().As<IOfferRepository>().WithParameter("connectionString", connectionString).SingleInstance();
            container.RegisterType<ChatRepository>().As<IChatRepository>().WithParameter("connectionString", connectionString).SingleInstance();
            container.RegisterType<MessageRepository>().As<IMessageRepository>().WithParameter("connectionString", connectionString).SingleInstance();

        }
    }

    public class MvcApplication : System.Web.HttpApplication
    {
        public static class ContainerProvider
        {
            public static IContainer Container { get; set; }

            public static T Resolve<T>() => Container == null ? default(T) : Container.Resolve<T>();
        }
        //DI
        private void InitContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            IoCContainer.InitContainer(builder);

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
        protected void Application_Start()
        {
            InitContainer();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


        }
    }
}
