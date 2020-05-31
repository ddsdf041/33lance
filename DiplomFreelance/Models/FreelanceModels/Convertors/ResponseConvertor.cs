using DiplomFreelance.Models.FreelanceModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.Convertors
{
    public static class ResponseConvertor
    {
        public static Response ConvertFromViewModelToDBModel(this CreateResponseViewModel response, string idExecutor)
        {
            Response item = new Response()
            {
                Price = response.Price,
                Notation = response.Notation,
                ID_Executor = idExecutor,
                ID_Order = response.ID_Order,
                Date = response.Date
            };
            return item;
        }
        public static ResponseViewModel ConvertToResponseViewModel(this DomainResponse response)
        {
            ResponseViewModel item = new ResponseViewModel()
            {
                ID = response.ID,
                Price = response.Price,
                Notation = response.Notation,
                Executor =response.Executor.ConvertToExecutorViewModel(),
                ID_Order = response.ID_Order,
                Date=response.Date
            };
            return item;
        }
        public static  List<ResponseViewModel> ConvertToResponseViewModel(this List<DomainResponse> response)
        {
            var list = new List<ResponseViewModel>();
            foreach (var item in response)
            {
                list.Add(item.ConvertToResponseViewModel());
            }
            return list;
        }

        public static DomainResponse ConvertToResponseDomainModel(this Response response, DomainExecutor executor)
        {
            DomainResponse item = new DomainResponse()
            {
                ID = response.ID,
                Price = response.Price,
                Notation = response.Notation,
                ID_Order = response.ID_Order,
                Executor = executor,
                Date = response.Date
            };
            return item;
        }
       
        //public static List<DomainResponse> ConvertToResponseDomainModel(this List<Response> response)
        //{
        //    var list = new List<DomainResponse>();
        //    foreach (var item in response)
        //    {
        //        list.Add(ConvertToResponseDomainModel(item));
        //    }
        //    return list;
        //}
    }
}