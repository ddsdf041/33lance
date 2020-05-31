using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DiplomFreelance.Controllers.BusinessLogic.IService;
using DiplomFreelance.Models.FreelanceModels;
using DiplomFreelance.Models.FreelanceModels.Convertors;
using DiplomFreelance.Models.Repository;
using DiplomFreelance.Models.Repository.Interfaces;

namespace DiplomFreelance.Controllers.BusinessLogic
{
    public class ServiceMeasure: IServiceMeasure
    {
        private IMeasureRepository _measureRepository;
        public ServiceMeasure(IMeasureRepository measureRepository)
        {
            _measureRepository = measureRepository;
        }

        public DomainMeasure GetMeasure(int id)
        {
            var item = _measureRepository.GetMeasureById(id);
            return item.ConvertToMeasureDomainModel();
        }
        public List<DomainMeasure> GetAllMeasure()
        {
            var item = _measureRepository.GetAllMeasure();
            return item.ToList().ConvertToMeasureDomainModel();
        }
    }
}