using DiplomFreelance.Models.FreelanceModels;
using DiplomFreelance.Models.FreelanceModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomFreelance.Controllers.BusinessLogic.IService
{
    public interface IServiceResponse
    {
        List<DomainResponse> GetResponseByExecutorId(string idExecutor);
        List<DomainResponse> GetResponseByOrderId(Guid idOrder);
        DomainResponse GetResponseByOrderIdAndExecutorId(Guid idOrder, string idExecutor);
        bool CreateNewResponse(CreateResponseViewModel responseVM, string idExecutor);
        bool DeleteResponseById(int idResponse);
    }
}
