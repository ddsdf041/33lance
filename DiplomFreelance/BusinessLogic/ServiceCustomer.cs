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
    public class ServiceCustomer: IServiceCustomer
    {
        private ICustomerRepository _customerRepository;

        public ServiceCustomer(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        private DomainCustomer GetCustomer(Customer customer)
        {
            return customer.ConvertToCustomerDomainModel();
        }

        public DomainCustomer GetCustomerByUserID(string userID)
        {
            return GetCustomer(_customerRepository.GetCustomerByUserId(userID));
        }
        public DomainCustomer GetCustomerByEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
                return null;
            return GetCustomer(_customerRepository.GetCustomerByEmail(email));
        }
        public void BannedCustomer(string idCustomer)
        {
            _customerRepository.BannedCustomer(idCustomer);
        }

        public void UnBannedCustomer(string idCustomer)
        {
            _customerRepository.UnBannedCustomer(idCustomer);
        }

        public void DeleteCustomer(string idCustomer)
        {
            _customerRepository.DeleteCustomer(idCustomer);
        }
        public void CreateCustomer(CustomerViewModel customer)
        {
            _customerRepository.CreateCustomer(customer.ConvertFromViewModelToDBModel());
        }

        public IEnumerable<DomainCustomer> GetAllCustomer()
        {
            return _customerRepository.GetAllCustomer().ToList().ConvertToCustomerDomainModel();
        }
    }
}