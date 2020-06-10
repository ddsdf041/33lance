using DiplomFreelance.Models.FreelanceModels;
using DiplomFreelance.Models.FreelanceModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Controllers.BusinessLogic.IService
{
    public interface IServiceOrder
    {
        List<DomainOrder> GetAllOrder();
        DomainOrder GetOrderById(Guid idOrder);
        List<DomainOrder> GetOrderViewModelsWhereExecutorIsNull();
        List<DomainOrder> GetOrdersByExecutorId(string idExecutor);
        List<DomainOrder> GetOrdersByCustomerId(string idCustomer);
        bool CreateNewOrder(CreateOrderViewModel orderViewModel, string idCustomer);
        bool DeleteOrderById(Guid idOrder);
        void BannedOrder(Guid idOrder);
        void UnBannedOrder(Guid idOrder);
        bool SetExecutorInOrder(Guid idOrder, string idExec, decimal budget);
        bool SetExecutorInOrder(Guid idOrder, string idExec);
        List<DomainOrder> OrderWithNullExecutorAndCurrentExecutorGiveAResponse(string idExecutor);
        bool SetOrderStatusIsReject(Guid idOrder);
        bool SetOrderStatusIsComplete(Guid idOrder);
        List<DomainOrder> GetOrdersOfferedToTheExecutorAndStatusIsPending(string idExecutor);
        List<DomainOrder> GetOrdersBySubcategoryId(int idSubcategory);
    }
}