
using DiplomFreelance.Models.FreelanceModels;
using DiplomFreelance.Models.FreelanceModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomFreelance.Controllers.BusinessLogic.IService
{
   public interface IServiceCustomer
    {
        DomainCustomer GetCustomerByUserID(string userID);
        void BannedCustomer(string idCustomer);
        void UnBannedCustomer(string idCustomer);
        IEnumerable<DomainCustomer> GetAllCustomer();
        DomainCustomer GetCustomerByEmail(string email);
        void DeleteCustomer(string idCustomer);
        void CreateCustomer(CustomerViewModel customer);
    }
}
