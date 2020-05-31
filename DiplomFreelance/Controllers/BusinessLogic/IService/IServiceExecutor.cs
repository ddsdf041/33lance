using DiplomFreelance.Models.FreelanceModels;
using DiplomFreelance.Models.FreelanceModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomFreelance.Controllers.BusinessLogic.IService
{
    public interface IServiceExecutor
    {
        List<DomainExecutor> GetExecutorsByIdSubcategory(int idsubcategory);
        List<DomainExecutor> GetExecutorsForServicesViewByNameSubcategory(string nameSubcategory);
        void BannedExecutor(string idExexutor);
        void UnBannedExecutor(string idExexutor);
        DomainExecutor GetExecutorByIdUser(string idUser);
        bool UpdateExecutorDetails(ExecutorViewModel newExecutor);
        DomainExecutor GetExecutorByEmail(string email);
    }
}
