using DiplomFreelance.Models.FreelanceModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.Convertors
{
    public static class CustomerConvertor
    {
        public static Customer ConvertFromViewModelToDBModel(this CustomerViewModel customer)
        {
            Customer item = new Customer()
            {
                ID_User = customer.ID,
                Name = customer.Name,
                Email = customer.Email
            };
            return item;
        }
        public static CustomerViewModel ConvertToCustomerViewModel(this DomainCustomer customer)
        {
            CustomerViewModel item = new CustomerViewModel()
            {
                ID = customer.ID_User,
                Name = customer.Name,
                Email = customer.Email,
                IsBanned = customer.IsBanned
            };
            return item;
        }
        public static List<CustomerViewModel> ConvertToCustomerViewModel(this List<DomainCustomer> customer)
        {
            var list = new List<CustomerViewModel>();
            foreach (var item in customer)
            {
                list.Add(ConvertToCustomerViewModel(item));
            }
            return list;
        }
        public static List<DomainCustomer> ConvertToCustomerDomainModel(this List<Customer> customer)
        {
            var list = new List<DomainCustomer>();
            foreach (var item in customer)
            {
                list.Add(ConvertToCustomerDomainModel(item));
            }
            return list;
        }
        public static DomainCustomer ConvertToCustomerDomainModel(this Customer customer)
        {

            DomainCustomer item = new DomainCustomer()
            {
                Name = customer.Name,
                ID_User = customer.ID_User,
                Email = customer.Email,
                IsBanned = customer.IsBanned
            };
            return item;
        }

    }
}