using DiplomFreelance.Controllers.BusinessLogic.IService;
using DiplomFreelance.Models.FreelanceModels;
using DiplomFreelance.Models.FreelanceModels.Convertors;
using DiplomFreelance.Models.FreelanceModels.ViewModel;
using DiplomFreelance.Models.Repository;
using DiplomFreelance.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Controllers.BusinessLogic
{
    public class ServiceOrder: IServiceOrder
    {
        private IOrderRepository _orderRepository;
        private IServiceCustomer _serviceCustomer;
        private IServiceFileOrder _fileService;
        private IServiceResponse _serviceResponse;
        private IServicePlace _servicePlace;
        private IServiceMeasure _serviceMeasure;
        private IServiceSubcategory _serviceSubcategory;
        private IServiceCategory _serviceCategory;
        private IServiceStatus _serviceStatus;
        private IServiceExecutor _serviceExecutor;
        private IServiceOffer _serviceOffer;
        public ServiceOrder(IOrderRepository orderRepository,
                            IServiceFileOrder serviceFileOrder,
                            IServiceResponse serviceResponse,
                            IServicePlace servicePlace,
                            IServiceMeasure serviceMeasure,
                            IServiceSubcategory serviceSubcategory,
                            IServiceCategory serviceCategory,
                            IServiceStatus serviceStatus,
                            IServiceExecutor serviceExecutor,
                            IServiceOffer serviceOffer,
                            IServiceCustomer serviceCustomer)
        {
            _orderRepository = orderRepository;
            _fileService = serviceFileOrder;
            _serviceResponse = serviceResponse;
            _servicePlace =servicePlace;
            _serviceMeasure = serviceMeasure;
            _serviceSubcategory = serviceSubcategory;
            _serviceCategory = serviceCategory;
            _serviceStatus = serviceStatus;
            _serviceExecutor = serviceExecutor;
            _serviceOffer = serviceOffer;
            _serviceCustomer = serviceCustomer;
        }
        private List<DomainOrder> GetOrders(List<Order> orders)
        {
            var ordersDomain = new List<DomainOrder>();
            foreach (var item in orders)
            {
                ordersDomain.Add(GetOrder(item));
            }
            return ordersDomain;
        }
        private DomainOrder GetOrder(Order order)
        {
            var files = _fileService.GetFileOrdersByOrderId(order.ID);
            var responses = _serviceResponse.GetResponseByOrderId(order.ID);
            var status = _serviceStatus.GetStatus(order.ID_Status);
            var subcategory = _serviceSubcategory.GetSubcategoryById(order.ID_Subcategory);
            var category = _serviceCategory.GetCategoryBySubcategoryId(order.ID_Subcategory);
            var place = _servicePlace.GetPlace(order.ID_Place);
            var measure = _serviceMeasure.GetMeasure(order.ID_Measure);
            var executor = _serviceExecutor.GetExecutorByIdUser(order.ID_Executor);
            var customer = _serviceCustomer.GetCustomerByUserID(order.ID_Customer);

            return order.ConvertToOrderDomainModel(customer, executor, status, subcategory, category, place, measure, files, responses);
        }

        public List<DomainOrder> GetAllOrder()
        {
           return GetOrders(_orderRepository.GetAllOrder().ToList());
        }
        public DomainOrder GetOrderById(Guid idOrder)
        {
            return GetOrder(_orderRepository.GetOrderById(idOrder));
        }
        public List<DomainOrder> GetOrderViewModelsWhereExecutorIsNull()
        {
            return GetOrders(_orderRepository.GetAllOrderWhereExecutorIsNull().ToList());
        }
       
        public List<DomainOrder> GetOrdersByExecutorId(string idExecutor)
        {
            return GetOrders(_orderRepository.GetOrderByExecutorId(idExecutor).ToList());
        }
        public List<DomainOrder> GetOrdersBySubcategoryId(int idSubcategory)
        {
            return GetOrders(_orderRepository.GetOrderBySubcategoryId(idSubcategory).ToList());
        }
        public List<DomainOrder> GetOrdersByCustomerId(string idCustomer)
        {
            return GetOrders(_orderRepository.GetOrderByCustomerId(idCustomer).ToList());
        }

        public List<DomainOrder> GetOrdersOfferedToTheExecutorAndStatusIsPending(string idExecutor)
        {
            return GetOrders(_orderRepository.GetAllOrderWhenOfferedToExecutor(idExecutor).ToList());
        }
        public bool SetOrderStatusIsComplete(Guid idOrder)
        {
            try
            {
                _orderRepository.UpdateStatusOrder(idOrder, 3);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetOrderStatusIsReject(Guid idOrder)
        {
            try
            {
                _orderRepository.UpdateStatusOrder(idOrder, 5);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<DomainOrder> OrderWithNullExecutorAndCurrentExecutorGiveAResponse(string idExecutor)
        {
            var response = _serviceResponse.GetResponseByExecutorId(idExecutor);

            return GetOrders(_orderRepository.GetAllOrderWhereExecutorIsNull().Where(y=>response.Any(x=>x.ID_Order ==y.ID)).ToList());
        }

        public bool CreateNewOrder(CreateOrderViewModel orderViewModel, string idCustomer)
        {
            //int customerID = _customerRepository.GetCustomerByUserID(userID).ID;

            try
            {
                if (orderViewModel.ID_Place != 3)
                    orderViewModel.Address = null;
                var order = orderViewModel.ConvertFromViewModelToDBModel(idCustomer);
                _orderRepository.CreateOrder(order);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool DeleteOrderById(Guid idOrder)
        {           
            try
            {
                var responses = _serviceResponse.GetResponseByOrderId(idOrder);
                foreach (var item in responses)
                {
                    _serviceResponse.DeleteResponseById(item.ID);
                }
                var offer = _serviceOffer.GetOfferByOrderId(idOrder);
                if (offer != null)
                {
                    _serviceOffer.DeleteOffer(offer.ID);
                }
                var files = _fileService.GetFileOrdersByOrderId(idOrder);
                foreach (var item in files)
                {
                    _fileService.DeleteFileOrder(item.ID);
                }
                _orderRepository.DeleteOrder(idOrder);              
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetExecutorInOrder(Guid idOrder, string idExec, decimal budget)
        {
            try
            {
                _orderRepository.UpdateExecutorOrder(idExec,idOrder, budget);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool SetExecutorInOrder(Guid idOrder, string idExec)
        {
            try
            {
                _orderRepository.UpdateExecutorOrder(idExec, idOrder);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void BannedOrder(Guid idOrder)
        {
            _orderRepository.BannedOrder(idOrder);
        }

        public void UnBannedOrder(Guid idOrder)
        {
            _orderRepository.UnBannedOrder(idOrder);
        }
    }
}