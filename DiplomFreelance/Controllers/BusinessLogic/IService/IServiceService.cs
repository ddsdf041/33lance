using DiplomFreelance.Models.FreelanceModels;
using DiplomFreelance.Models.FreelanceModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomFreelance.Controllers.BusinessLogic.IService
{
    public interface IServiceService
    {
        List<DomainService> GetServicesByExecutorID(string idExecutor);
        DomainService GetServicesByID(int id);
        bool CreateNewService(CreateServiceViewModel item, string idExecutor);
        bool UpdateService(CreateServiceViewModel item, string idExecutor);
        bool DeleteService(int id);
    }
}
