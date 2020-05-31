
using DiplomFreelance.Models.FreelanceModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.Convertors
{
    public static class OrderConvertor
    {

        public static Order ConvertFromViewModelToDBModel(this CreateOrderViewModel order, string idcustomer)
        {
            Order item = new Order()
            {
                ID = order.ID,
                Name = order.Name,
                ID_Subcategory = order.ID_Subcategory,
                Description = order.Description,
                Deadline = order.Deadline,
                ID_Measure = order.ID_Measure,
                ID_Executor = order.ID_Executor,
                Budget = order.Budget,
                ID_Place = order.ID_Place,
                Address = order.Address,
                ID_Status = String.IsNullOrEmpty(order.ID_Executor) ? 1 : 4,
                ID_Customer = idcustomer
            };
            return item;
        }
        public static OrderViewModel ConvertToOrderViewModel(this DomainOrder order)
        {
            OrderViewModel item = new OrderViewModel()
            {
                Name = order.Name,
                Description = order.Description,
                Deadline = order.Deadline,
                Budget = order.Budget,
                Address = order.Address,
                ID = order.ID,
                Customer = order.Customer.ConvertToCustomerViewModel(),
                Executor = order.Executor == null ? null : order.Executor.ConvertToExecutorViewModel(),
                Category = order.Category.Name,
                Files = order.Files.ConvertToFileOrderViewModel(),
                Measure = order.Measure.Name,
                Place = order.Place.Name,
                Status = order.Status.Name,
                ID_Status = order.Status.ID,
                Responses = order.Responses.ConvertToResponseViewModel(),
                Subcategory = order.Subcategory.Name,
                Date = order.Date,
                IsBanned = order.IsBanned
            };
            return item;
        }
        public static List<OrderViewModel> ConvertToOrderViewModel(this List<DomainOrder> order)
        {
            var list = new List<OrderViewModel>();
            foreach (var item in order)
            {
                list.Add(ConvertToOrderViewModel(item));
            }
            return list;
        }
        public static DomainOrder ConvertToOrderDomainModel(this Order order, DomainCustomer customer, DomainExecutor executor, DomainStatus status, DomainSubcategory subcategory, DomainCategory category, DomainPlace place, DomainMeasure measure, List<DomainFileOrder> files, List<DomainResponse> responses)
        {
            DomainOrder item = new DomainOrder()
            {
                Name = order.Name,
                Description = order.Description,
                Deadline = order.Deadline,
                Budget = order.Budget,
                Address = order.Address,
                Status = status,
                Executor = executor,
                Customer = customer,
                Subcategory = subcategory,
                Category = category,
                Files = files,
                ID = order.ID,
                Place = place,
                Measure = measure,
                Responses = responses,
                Date = order.Date,
                IsBanned = order.IsBanned
            };
            return item;
        }

    }
}