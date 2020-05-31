using DiplomFreelance.Models.FreelanceModels;
using DiplomFreelance.Models.Repository;
using DiplomFreelance.Models.FreelanceModels.Convertors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DiplomFreelance.Controllers.BusinessLogic.IService;
using DiplomFreelance.Models.Repository.Interfaces;

namespace DiplomFreelance.Controllers.BusinessLogic
{
    public class ServicePlace: IServicePlace
    {
        private IPlaceRepository _placeRepository;
        public ServicePlace(IPlaceRepository placeRepository)
        {
            _placeRepository = placeRepository;
        }

        public DomainPlace GetPlace(int id)
        {
            var item = _placeRepository.GetPlaceById(id);

            return item.ConvertToPlaceDomainModel();
        }

        public List<DomainPlace> GetAllPlace()
        {
            var item = _placeRepository.GetAllPlace();
            return item.ToList().ConvertToPlaceDomainModel();
        }
    }
}