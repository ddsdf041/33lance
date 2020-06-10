using DiplomFreelance.Controllers.BusinessLogic.IService;
using DiplomFreelance.Models.FreelanceModels;
using DiplomFreelance.Models.FreelanceModels.DBModel;
using DiplomFreelance.Models.FreelanceModels.DomainModel;
using DiplomFreelance.Models.FreelanceModels.ViewModel;
using DiplomFreelance.Models.FreelanceModels.Convertors;
using DiplomFreelance.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectForTests.Fakes
{
    class FakeServiceCategory : IServiceCategory
    {
        public bool CreateCategory(Category item)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCategory(int id)
        {
            throw new NotImplementedException();
        }

        public List<DomainCategory> GetCategories()
        {
            throw new NotImplementedException();
        }

        public DomainCategory GetCategoryById(int id)
        {
            throw new NotImplementedException();
        }

        public DomainCategory GetCategoryBySubcategoryId(int idSub)
        {
            throw new NotImplementedException();
        }

        public bool UpdateCategory(Category item)
        {
            throw new NotImplementedException();
        }
    }
    class FakeServiceSubcategory : IServiceSubcategory
    {
        public bool CreateSubcategory(Subcategory item)
        {
            throw new NotImplementedException();
        }

        public bool DeleteSubcategory(int id)
        {
            throw new NotImplementedException();
        }

        public List<DomainSubcategory> GetSubcategories(int category)
        {
            throw new NotImplementedException();
        }

        public DomainSubcategory GetSubcategoryById(int idSub)
        {
            throw new NotImplementedException();
        }

        public bool UpdateSubcategory(Subcategory item)
        {
            throw new NotImplementedException();
        }
    }
    class FakeServiceMeasure : IServiceMeasure
    {
        public List<DomainMeasure> GetAllMeasure()
        {
            throw new NotImplementedException();
        }

        public DomainMeasure GetMeasure(int id)
        {
            throw new NotImplementedException();
        }
    }
    class FakeServicePlace : IServicePlace
    {
        public List<DomainPlace> GetAllPlace()
        {
            throw new NotImplementedException();
        }

        public DomainPlace GetPlace(int id)
        {
            throw new NotImplementedException();
        }
    }
    class FakeRepositoryService : IServiceRepository
    {
        public void CreateService(Service item)
        {
            FakeDataBase.Services.Add(item);
        }
        public void DeleteService(int id)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Service> GetAllService()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Service> GetServiceByExecutorID(string idexec)
        {
            throw new NotImplementedException();
        }
        public Service GetServiceById(int id)
        {
            throw new NotImplementedException();
        }
        public void UpdateService(Service item)
        {
            throw new NotImplementedException();
        }
    }

    class FakeServiceOrder : IServiceOrder
    {
        public void BannedOrder(Guid idOrder)
        {
            throw new NotImplementedException();
        }

        public bool CreateNewOrder(CreateOrderViewModel orderViewModel, string idCustomer)
        {
            throw new NotImplementedException();
        }

        public bool DeleteOrderById(Guid idOrder)
        {
            throw new NotImplementedException();
        }

        public List<DomainOrder> GetAllOrder()
        {
            throw new NotImplementedException();
        }

        public DomainOrder GetOrderById(Guid idOrder)
        {
            throw new NotImplementedException();
        }

        public List<DomainOrder> GetOrdersByCustomerId(string idCustomer)
        {
            throw new NotImplementedException();
        }

        public List<DomainOrder> GetOrdersByExecutorId(string idExecutor)
        {
            throw new NotImplementedException();
        }

        public List<DomainOrder> GetOrdersBySubcategoryId(int idSubcategory)
        {
            throw new NotImplementedException();
        }

        public List<DomainOrder> GetOrdersOfferedToTheExecutorAndStatusIsPending(string idExecutor)
        {
            throw new NotImplementedException();
        }

        public List<DomainOrder> GetOrderViewModelsWhereExecutorIsNull()
        {
            throw new NotImplementedException();
        }

        public List<DomainOrder> OrderWithNullExecutorAndCurrentExecutorGiveAResponse(string idExecutor)
        {
            throw new NotImplementedException();
        }

        public bool SetExecutorInOrder(Guid idOrder, string idExec, decimal budget)
        {
            throw new NotImplementedException();
        }

        public bool SetExecutorInOrder(Guid idOrder, string idExec)
        {
            throw new NotImplementedException();
        }

        public bool SetOrderStatusIsComplete(Guid idOrder)
        {
            throw new NotImplementedException();
        }

        public bool SetOrderStatusIsReject(Guid idOrder)
        {
            throw new NotImplementedException();
        }

        public void UnBannedOrder(Guid idOrder)
        {
            throw new NotImplementedException();
        }
    }

    class FakeServiceFileOrder : IServiceFileOrder
    {
        public bool CreateNewFileOrder(FileOrderViewModel fileOrder)
        {
            throw new NotImplementedException();
        }

        public bool DeleteFileOrder(int id)
        {
            throw new NotImplementedException();
        }

        public List<DomainFileOrder> GetFileOrdersByOrderId(Guid idOrder)
        {
            var list = new List<DomainFileOrder>();
            var fileOrders = FakeDataBase.FileOrders.Where(x => x.ID_Order == idOrder);
            foreach (var item in fileOrders)
            {
                list.Add(item.ConvertToFileOrderDomainModel());
            }
            return list;
        }
    }

    class FakeServiceResponse : IServiceResponse
    {
        public bool CreateNewResponse(CreateResponseViewModel responseVM, string idExecutor)
        {
            throw new NotImplementedException();
        }

        public bool DeleteResponseById(int idResponse)
        {
            var item = FakeDataBase.Responses.Find(x=>x.ID == idResponse);
            FakeDataBase.Responses.Remove(item);
            return true;
        }

        public List<DomainResponse> GetResponseByExecutorId(string idExecutor)
        {
            throw new NotImplementedException();
        }

        public List<DomainResponse> GetResponseByOrderId(Guid idOrder)
        {
            var list = new List<DomainResponse>();
            var responses = FakeDataBase.Responses.Where(x => x.ID_Order == idOrder);
            foreach (var item in responses)
            {
                list.Add(item.ConvertToResponseDomainModel(new DomainExecutor()));
            }
            return list;
        }

        public DomainResponse GetResponseByOrderIdAndExecutorId(Guid idOrder, string idExecutor)
        {
            throw new NotImplementedException();
        }
    }

    class FakeServiceStatus : IServiceStatus
    {
        public DomainStatus GetStatus(int id)
        {
            throw new NotImplementedException();
        }
    }

    class FakeServiceExecutor : IServiceExecutor
    {
        public void BannedExecutor(string idExexutor)
        {
            throw new NotImplementedException();
        }

        public DomainExecutor GetExecutorByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public DomainExecutor GetExecutorByIdUser(string idUser)
        {
            throw new NotImplementedException();
        }

        public List<DomainExecutor> GetExecutorsByIdSubcategory(int idsubcategory)
        {
            throw new NotImplementedException();
        }

        public List<DomainExecutor> GetExecutorsForServicesViewByNameSubcategory(string nameSubcategory)
        {
            throw new NotImplementedException();
        }

        public List<DomainExecutor> GetExecutorsForServicesViewByServiceName(string nameSubcategory)
        {
            throw new NotImplementedException();
        }

        public void UnBannedExecutor(string idExexutor)
        {
            throw new NotImplementedException();
        }

        public bool UpdateExecutorDetails(ExecutorViewModel newExecutor)
        {
            throw new NotImplementedException();
        }
    }

    class FakeServiceOffer : IServiceOffer
    {
        public bool CreateNewOffer(OfferViewModel item)
        {
            throw new NotImplementedException();
        }

        public bool DeleteOffer(int id)
        {
            var item = FakeDataBase.Offers.Find(x => x.ID == id);
            FakeDataBase.Offers.Remove(item);
            return true;
        }

        public DomainOffer GetOfferById(int id)
        {
            throw new NotImplementedException();
        }

        public DomainOffer GetOfferByOrderId(Guid id)
        {
            return FakeDataBase.Offers.Find(x=>x.ID_Order == id).ConvertToCustomerDomainModel();
        }

        public List<DomainOffer> GetOffersByExecutorId(string idExecutor)
        {
            throw new NotImplementedException();
        }
    }

    class FakeServiceCustomer : IServiceCustomer
    {
        public void BannedCustomer(string idCustomer)
        {
            throw new NotImplementedException();
        }

        public void CreateCustomer(CustomerViewModel customer)
        {
            throw new NotImplementedException();
        }

        public void DeleteCustomer(string idCustomer)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DomainCustomer> GetAllCustomer()
        {
            throw new NotImplementedException();
        }

        public DomainCustomer GetCustomerByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public DomainCustomer GetCustomerByUserID(string userID)
        {
            throw new NotImplementedException();
        }

        public void UnBannedCustomer(string idCustomer)
        {
            throw new NotImplementedException();
        }
    }

    class FakeRepositoryOrder : IOrderRepository
    {
        public void BannedOrder(Guid id)
        {
            throw new NotImplementedException();
        }

        public void CreateOrder(Order item)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrder(Guid id)
        {
            FakeDataBase.Orders.Find(x => x.ID == id);
        }

        public IEnumerable<Order> GetAllOrder()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAllOrderWhenOfferedToExecutor(string idExecutor)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAllOrderWhereExecutorIsNull()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetOrderByCustomerId(string idcustomer)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetOrderByExecutorId(string idexec)
        {
            throw new NotImplementedException();
        }

        public Order GetOrderById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetOrderBySubcategoryId(int idSubcategory)
        {
            throw new NotImplementedException();
        }

        public void UnBannedOrder(Guid id)
        {
            throw new NotImplementedException();
        }

        public void UpdateExecutorOrder(string idExecutor, Guid idOrder, decimal budget)
        {
            throw new NotImplementedException();
        }

        public void UpdateExecutorOrder(string idExecutor, Guid idOrder)
        {
            throw new NotImplementedException();
        }

        public void UpdateStatusOrder(Guid idOrder, int status)
        {
            throw new NotImplementedException();
        }
    }

    class FakeDataBase
    {
        public static List<Service> Services { get; set; }
        public static List<Order> Orders { get; set; }
        public static List<Response> Responses { get; set; }
        public static List<Offer> Offers { get; set; }
        public static List<FileOrder> FileOrders { get; set; }

        public static  void CreateFake()
        {
            Services = new List<Service>();
        }

        public static void CreateFakeOrders()
        {
            
            Order order = new Order
            {
                ID = new Guid ("1648e8eb-b67e-4d3e-9589-a2059fb053df"),
                
            };

            Response response = new Response
            {
                ID = 1,
                ID_Order = new Guid("1648e8eb-b67e-4d3e-9589-a2059fb053df")
            };

            Offer offer = new Offer
            {
                ID = 2,
                ID_Order = new Guid("1648e8eb-b67e-4d3e-9589-a2059fb053df")
            };

            FileOrder fileOrder = new FileOrder
            {
                ID = 1,
                ID_Order = new Guid("1648e8eb-b67e-4d3e-9589-a2059fb053df")
            };

            Orders = new List<Order>();
            Responses = new List<Response>();
            Offers = new List<Offer>();
            FileOrders = new List<FileOrder>();

            Orders.Add(order);
            Responses.Add(response);
            Offers.Add(offer);
            FileOrders.Add(fileOrder);
        }

    }
}
