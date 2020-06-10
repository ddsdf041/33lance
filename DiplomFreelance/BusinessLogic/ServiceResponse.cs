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
    public class ServiceResponse: IServiceResponse
    {
        static IResponseRepository _responseRepository;
        static IServiceExecutor _serviceExecutor;

        public ServiceResponse(IResponseRepository responseRepository, IServiceExecutor serviceExecutor)
        {
            _responseRepository = responseRepository;
            _serviceExecutor = serviceExecutor;
        }

        private static List<DomainResponse> GetResponses(List<Response> orders)
        {
            var responseDomain = new List<DomainResponse>();
            foreach (var item in orders)
            {
                responseDomain.Add(GetResponse(item));
            }
            return responseDomain;
        }
        private static DomainResponse GetResponse(Response response)
        {
            var executor = _serviceExecutor.GetExecutorByIdUser(response.ID_Executor);
            return response.ConvertToResponseDomainModel(executor);
        }

        public List<DomainResponse> GetResponseByExecutorId(string idExecutor)
        {
            return GetResponses(_responseRepository.GetResponseByExecutorId(idExecutor).ToList());
        }
        public List<DomainResponse> GetResponseByOrderId(Guid idOrder)
        {
            return GetResponses(_responseRepository.GetResponseByOrderId(idOrder).ToList());
        }
        public DomainResponse GetResponseByOrderIdAndExecutorId(Guid idOrder, string idExecutor)
        {
            return GetResponse(_responseRepository.GetResponseByOrderIdAndExecutorId(idOrder, idExecutor));
        }
        public bool CreateNewResponse(CreateResponseViewModel responseVM, string idExecutor)
        {
            try
            {
                var response = responseVM.ConvertFromViewModelToDBModel(idExecutor);
                _responseRepository.CreateResponse(response);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteResponseById(int idResponse)
        {
            try
            {

                _responseRepository.DeleteResponse(idResponse);
                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}