using DiplomFreelance.Controllers.BusinessLogic;
using DiplomFreelance.Controllers.BusinessLogic.IService;
using DiplomFreelance.Models.FreelanceModels.ViewModel;
using DiplomFreelance.Models.Repository.Interfaces;
using NUnit.Framework;
using ProjectForTests.Fakes;
using System.Linq;

namespace urProject.Tests
{
    [TestFixture]
    public class UnitTextExecutor
    {
        [Test]
        public static void CreateNewService_IDPlace1_AddressNull()
        {
            CreateServiceViewModel item = new CreateServiceViewModel()
            {
                ID = 1,
                ID_Place = 1,
                Address = "Адрес какой-то"
            };

            FakeDataBase.CreateFake(); // инициализируем типо фейк "базу"

            IServiceRepository fakeServiceRepository = new FakeRepositoryService(); //создаем фейк репозиторий;
            //репозитории чисто для входных параметров проверяемого сервиса
            IServicePlace fakePlaceRepository = new FakeServicePlace();
            IServiceMeasure fakeMeasureRepository = new FakeServiceMeasure();
            IServiceCategory fakeCategoryRepository = new FakeServiceCategory();
            IServiceSubcategory fakeSubcategoryRepository = new FakeServiceSubcategory();

            //создаем тестируемый короче штука
            IServiceService serviceService = new ServiceService(fakeServiceRepository, fakePlaceRepository, fakeMeasureRepository, fakeSubcategoryRepository, fakeCategoryRepository);

            serviceService.CreateNewService(item, "someID");

            //достаем из фейк базы типо наш айтем 
            var itemAfterMethod = FakeDataBase.Services.Find(x => x.ID_Executor == "someID");


            //проверочка
            Assert.AreEqual(itemAfterMethod.Address, null);
        }
        [Test]
        public static void CreateNewService_IDPlace3_AddressNotNull()
        {
            CreateServiceViewModel item = new CreateServiceViewModel()
            {
                ID = 1,
                ID_Place = 3,
                Address = "Адрес какой-то"
            };

            FakeDataBase.CreateFake(); // инициализируем типо фейк "базу"

            IServiceRepository fakeServiceRepository = new FakeRepositoryService(); //создаем фейк репозиторий;
            //репозитории чисто для входных параметров проверяемого сервиса
            IServicePlace fakePlaceRepository = new FakeServicePlace();
            IServiceMeasure fakeMeasureRepository = new FakeServiceMeasure();
            IServiceCategory fakeCategoryRepository = new FakeServiceCategory();
            IServiceSubcategory fakeSubcategoryRepository = new FakeServiceSubcategory();

            //создаем тестируемый короче штука
            IServiceService serviceService = new ServiceService(fakeServiceRepository, fakePlaceRepository, fakeMeasureRepository, fakeSubcategoryRepository, fakeCategoryRepository);

            serviceService.CreateNewService(item, "someID");

            //достаем из фейк базы типо наш айтем 
            var itemAfterMethod = FakeDataBase.Services.Find(x => x.ID_Executor == "someID");


            //проверочка
            Assert.AreEqual(itemAfterMethod.Address, "Адрес какой-то");
        }
        public static void Main()
        {
           
        }

    }
}