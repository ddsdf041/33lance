using DiplomFreelance.Models.FreelanceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomFreelance.Models.Repository.Interfaces
{
    public interface ICustomerRepository
    {
        Customer GetCustomerByEmail(string email);
        IEnumerable<Customer> GetAllCustomer();
        void CreateCustomer(Customer item); // создание объекта
        void UpdateCustomer(Customer item); // обновление объекта
        void DeleteCustomer(string id_User); // удаление объекта по id
        Customer GetCustomerByUserId(string id_User);
        void BannedCustomer(string id);
        void UnBannedCustomer(string id);
    }
}
