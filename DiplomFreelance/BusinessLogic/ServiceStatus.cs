using DiplomFreelance.Controllers.BusinessLogic.IService;
using DiplomFreelance.Models.FreelanceModels;
using DiplomFreelance.Models.FreelanceModels.Convertors;
using DiplomFreelance.Models.Repository;
using DiplomFreelance.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Controllers.BusinessLogic
{
    public class ServiceStatus: IServiceStatus
    {
        private IStatusRepository _statusRepository;
        public ServiceStatus(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public DomainStatus GetStatus(int id)
        {
            var item = _statusRepository.GetStatusById(id);

            return item.ConvertToStatusDomainModel();
        }

    }
}