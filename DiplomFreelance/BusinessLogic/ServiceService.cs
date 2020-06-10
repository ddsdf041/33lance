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
    public class ServiceService: IServiceService
    {
        private IServiceRepository _serviceRepository;
        private IServicePlace _servicePlace;
        private IServiceMeasure _serviceMeasure;
        private IServiceSubcategory _serviceSubcategory;
        private IServiceCategory _serviceCategory;

        public ServiceService(IServiceRepository serviceRepository,
            IServicePlace servicePlace,
            IServiceMeasure serviceMeasure,
            IServiceSubcategory serviceSubcategory,
            IServiceCategory serviceCategory)
        {
            _serviceRepository = serviceRepository;
            _servicePlace = servicePlace;
            _serviceMeasure = serviceMeasure;
            _serviceSubcategory = serviceSubcategory;
            _serviceCategory = serviceCategory;
        }


        private DomainService GetService(Service service)
        {
            var place = _servicePlace.GetPlace(service.Place);
            var measure = _serviceMeasure.GetMeasure(service.ID_Measure);
            var subcategory = _serviceSubcategory.GetSubcategoryById(service.ID_Subcategory);
            var category = _serviceCategory.GetCategoryBySubcategoryId(subcategory.ID);
            return service.ConvertToServiceDomainModel(measure, place, subcategory, category);
        }

        private List<DomainService> GetServices(List<Service> services)
        {
            var servicesDomain = new List<DomainService> { };
            foreach (var item in services)
            {
                servicesDomain.Add(GetService(item));
            }
            return servicesDomain;
        }

        public List<DomainService> GetServicesByExecutorID(string idExecutor)
        {
            
            return GetServices(_serviceRepository.GetServiceByExecutorID(idExecutor).ToList());
        }

        public DomainService GetServicesByID(int id)
        {
            return GetService(_serviceRepository.GetServiceById(id));
        }

        public bool CreateNewService(CreateServiceViewModel item, string idExecutor)
        {
            try
            {
                if (item.ID_Place != 3)
                    item.Address = null;
                var service = item.ConvertFromViewModelToDBModelForCreate(idExecutor);
                _serviceRepository.CreateService(service);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateService(CreateServiceViewModel item, string idExecutor)
        {
            try
            {
                if (item.ID_Place != 3)
                    item.Address = null;
                var service = item.ConvertFromViewModelToDBModelForUpdate(idExecutor);
                _serviceRepository.UpdateService(service);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteService(int id)
        {
            try
            {
                _serviceRepository.DeleteService(id);
                return true;
            }
            catch
            {
                return false;
            }
        }
      
    }
}