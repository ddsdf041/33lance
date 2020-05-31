using DiplomFreelance.Controllers.BusinessLogic;
using DiplomFreelance.Controllers.BusinessLogic.IService;
using DiplomFreelance.Models.Repository.Interfaces;
using NUnit.Framework;
using ProjectForTests.Fakes;
using System;
using System.Linq;


namespace ProjectForTests.Tests
{
    [TestFixture]
    class UnitTestCustomer
    {
        [Test]
        public static void DeleteOrderById_HaveResponse_ResponseDelete()
        {
            
            FakeDataBase.CreateFakeOrders(); // инициализируем типо фейк "базу"

            IOrderRepository fakeOrderRepository = new FakeRepositoryOrder(); //создаем фейк репозиторий;
            //сервисы чисто для входных параметров проверяемого сервиса
            IServiceResponse fakeResponseService = new FakeServiceResponse();
            IServiceOffer fakeOfferService = new FakeServiceOffer();
            IServicePlace fakePlaceService = new FakeServicePlace();
            IServiceStatus fakeStatusService = new FakeServiceStatus();
            IServiceFileOrder fakefileOrderService = new FakeServiceFileOrder();
            IServiceMeasure fakeMeasureService = new FakeServiceMeasure();
            IServiceCategory fakeCategoryService = new FakeServiceCategory();
            IServiceSubcategory fakeSubcategoryService = new FakeServiceSubcategory();
            IServiceCustomer fakeCustomerService = new FakeServiceCustomer();
            IServiceExecutor fakeExecutorService = new FakeServiceExecutor();
           

            //создаем тестируемый короче штука
            IServiceOrder serviceOrder = new ServiceOrder(fakeOrderRepository, fakefileOrderService, fakeResponseService,
                fakePlaceService, fakeMeasureService, fakeSubcategoryService, fakeCategoryService, fakeStatusService,
                fakeExecutorService, fakeOfferService, fakeCustomerService);

            serviceOrder.DeleteOrderById(new Guid("1648e8eb-b67e-4d3e-9589-a2059fb053df"));

            //достаем из фейк базы типо наш айтем 
            var itemAfterMethod = FakeDataBase.Responses.Where(x => x.ID_Order == new Guid("1648e8eb-b67e-4d3e-9589-a2059fb053df")).Count();

            //проверочка
            Assert.AreEqual(itemAfterMethod, 0);
        }

        [Test]
        public static void DeleteOrderById_HaveOffer_OfferDelete()
        {

            FakeDataBase.CreateFakeOrders(); // инициализируем типо фейк "базу"

            IOrderRepository fakeOrderRepository = new FakeRepositoryOrder(); //создаем фейк репозиторий;
            //сервисы чисто для входных параметров проверяемого сервиса
            IServiceResponse fakeResponseService = new FakeServiceResponse();
            IServiceOffer fakeOfferService = new FakeServiceOffer();
            IServicePlace fakePlaceService = new FakeServicePlace();
            IServiceStatus fakeStatusService = new FakeServiceStatus();
            IServiceFileOrder fakefileOrderService = new FakeServiceFileOrder();
            IServiceMeasure fakeMeasureService = new FakeServiceMeasure();
            IServiceCategory fakeCategoryService = new FakeServiceCategory();
            IServiceSubcategory fakeSubcategoryService = new FakeServiceSubcategory();
            IServiceCustomer fakeCustomerService = new FakeServiceCustomer();
            IServiceExecutor fakeExecutorService = new FakeServiceExecutor();


            //создаем тестируемый короче штука
            IServiceOrder serviceOrder = new ServiceOrder(fakeOrderRepository, fakefileOrderService, fakeResponseService,
                fakePlaceService, fakeMeasureService, fakeSubcategoryService, fakeCategoryService, fakeStatusService,
                fakeExecutorService, fakeOfferService, fakeCustomerService);

            serviceOrder.DeleteOrderById(new Guid("1648e8eb-b67e-4d3e-9589-a2059fb053df"));

            //достаем из фейк базы типо наш айтем 
            var itemAfterMethod = FakeDataBase.Offers.Where(x => x.ID_Order == new Guid("1648e8eb-b67e-4d3e-9589-a2059fb053df")).Count();

            //проверочка
            Assert.AreEqual(itemAfterMethod, 0);
        }

        [Test]
        public static void DeleteOrderById_HaveFileOrder_FileOrderDelete()
        {

            FakeDataBase.CreateFakeOrders(); // инициализируем типо фейк "базу"

            IOrderRepository fakeOrderRepository = new FakeRepositoryOrder(); //создаем фейк репозиторий;
            //сервисы чисто для входных параметров проверяемого сервиса
            IServiceResponse fakeResponseService = new FakeServiceResponse();
            IServiceOffer fakeOfferService = new FakeServiceOffer();
            IServicePlace fakePlaceService = new FakeServicePlace();
            IServiceStatus fakeStatusService = new FakeServiceStatus();
            IServiceFileOrder fakefileOrderService = new FakeServiceFileOrder();
            IServiceMeasure fakeMeasureService = new FakeServiceMeasure();
            IServiceCategory fakeCategoryService = new FakeServiceCategory();
            IServiceSubcategory fakeSubcategoryService = new FakeServiceSubcategory();
            IServiceCustomer fakeCustomerService = new FakeServiceCustomer();
            IServiceExecutor fakeExecutorService = new FakeServiceExecutor();


            //создаем тестируемый короче штука
            IServiceOrder serviceOrder = new ServiceOrder(fakeOrderRepository, fakefileOrderService, fakeResponseService,
                fakePlaceService, fakeMeasureService, fakeSubcategoryService, fakeCategoryService, fakeStatusService,
                fakeExecutorService, fakeOfferService, fakeCustomerService);

            serviceOrder.DeleteOrderById(new Guid("1648e8eb-b67e-4d3e-9589-a2059fb053df"));

            //достаем из фейк базы типо наш айтем 
            var itemAfterMethod = FakeDataBase.FileOrders.Where(x => x.ID_Order == new Guid("1648e8eb-b67e-4d3e-9589-a2059fb053df")).Count();

            //проверочка
            Assert.AreEqual(itemAfterMethod, 0);
        }
    }
}
