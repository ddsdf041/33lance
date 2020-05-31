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
    public class ServiceExecutor: IServiceExecutor
    {
        private IExecutorRepository _executorRepository;
        private IServiceService _serviceService;

        public ServiceExecutor(IExecutorRepository executorRepository, IServiceService serviceService)
        {
            _executorRepository = executorRepository;
            _serviceService = serviceService;
        }


   

        public List<DomainExecutor> GetExecutorsByIdSubcategory(int idsubcategory)
        {
            return GetExecutors(_executorRepository.GetExecutorsBySubId(idsubcategory).ToList());
        }
        public List<DomainExecutor> GetExecutorsForServicesViewByNameSubcategory(string nameSubcategory)
        {
            return GetExecutors(_executorRepository.GetExecutorsBySubName(nameSubcategory).ToList());
            
        }

        private DomainExecutor GetExecutor(Executor executor)
        {
            var services = _serviceService.GetServicesByExecutorID(executor.ID_User);
            return executor.ConvertToExecutorDomainModel(services);
        }
        private List<DomainExecutor> GetExecutors(List<Executor> executor)
        {
            var executors = new List<DomainExecutor> { };
            foreach (var item in executor)
            {
                executors.Add(GetExecutor(item));
            }
            return executors;
        }

        public DomainExecutor GetExecutorByIdUser(string idUser)
        {
            var dbExecutor = _executorRepository.GetByUserId(idUser);
            return GetExecutor(dbExecutor);
        }
        public DomainExecutor GetExecutorByID(string idExecutor)
        {
            if (String.IsNullOrEmpty(idExecutor))
                return null;
            return GetExecutor(_executorRepository.GetByUserId(idExecutor));
        }
        public DomainExecutor GetExecutorByEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
                return null;
            return GetExecutor(_executorRepository.GetExecutirByEmail(email));
        }

        public bool UpdateExecutorDetails(ExecutorViewModel newExecutor)
        {
            try
            {
                _executorRepository.UpdateExecutor(newExecutor.ConvertFromViewModelToDBModel());
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void BannedExecutor(string idExexutor)
        {
            _executorRepository.BannedExecutor(idExexutor);
        }

        public void UnBannedExecutor(string idExexutor)
        {
            _executorRepository.UnBannedExecutor(idExexutor);
        }
    }
}