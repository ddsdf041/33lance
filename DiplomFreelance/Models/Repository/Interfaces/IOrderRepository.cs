using DiplomFreelance.Models.FreelanceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomFreelance.Models.Repository.Interfaces
{
    public interface IOrderRepository 
    {
        IEnumerable<Order> GetAllOrder();
        Order GetOrderById(Guid id);
        void CreateOrder(Order item); // создание объекта
        void UpdateExecutorOrder(string idExecutor, Guid idOrder, decimal budget);
        void UpdateExecutorOrder(string idExecutor, Guid idOrder);
        void DeleteOrder(Guid id); // удаление объекта по id
        IEnumerable<Order> GetOrderByExecutorId(string idexec);
        IEnumerable<Order> GetOrderBySubcategoryId(int idSubcategory);
        IEnumerable<Order> GetAllOrderWhereExecutorIsNull();
        IEnumerable<Order> GetOrderByCustomerId(string idcustomer);
        void BannedOrder(Guid id);
        void UnBannedOrder(Guid id);
        void UpdateStatusOrder(Guid idOrder, int status);
        IEnumerable<Order> GetAllOrderWhenOfferedToExecutor(string idExecutor);

    }
}