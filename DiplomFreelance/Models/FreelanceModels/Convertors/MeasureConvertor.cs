using DiplomFreelance.Models.FreelanceModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.Convertors
{
    public static class MeasureConvertor
    {
        public static MeasureViewModel ConvertToMeasureViewModel(this DomainMeasure measure)
        {
            MeasureViewModel item = new MeasureViewModel()
            {
                ID = measure.ID,
                Name = measure.Name
            };
            return item;
        }
        public static List<MeasureViewModel> ConvertToMeasureViewModel(this List<DomainMeasure> measure)
        {
            List<MeasureViewModel> list = new List<MeasureViewModel>() { };
            foreach (var item in measure)
            {
                list.Add(item.ConvertToMeasureViewModel());
            }
            return list;
        }
        public static DomainMeasure ConvertToMeasureDomainModel(this Measure measure)
        {
            DomainMeasure item = new DomainMeasure()
            {
                ID = measure.ID,
                Name = measure.Name
            };
            return item;
        }
        public static List<DomainMeasure> ConvertToMeasureDomainModel(this List<Measure> measure)
        {
            List<DomainMeasure> list = new List<DomainMeasure>() { };
            foreach (var item in measure)
            {
                list.Add(item.ConvertToMeasureDomainModel());
            }
            return list;
        }
    }
}