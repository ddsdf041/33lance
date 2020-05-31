using DiplomFreelance.Models.FreelanceModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.Convertors
{
    public static class ServiceConvertor
    {
        public static Service ConvertFromViewModelToDBModelForCreate(this CreateServiceViewModel service, string idExecutor)
        {
            Service item = new Service()
            {
                ID_Executor = idExecutor,
                ID_Subcategory = service.ID_Subcategory,
                ID_Measure = service.ID_Measure,
                Address = service.Address,
                 Name = service.Name,
                 Expirience = service.Expirience,
                 Notation = service.Notation,
                 Place = service.ID_Place,
                 Time_work = service.Time_work,
                 Price = service.Price
            };
            return item;
        }
        public static Service ConvertFromViewModelToDBModelForUpdate(this CreateServiceViewModel service, string idExecutor)
        {
            Service item = service.ConvertFromViewModelToDBModelForCreate(idExecutor);
            item.ID = service.ID;
            return item;
        }

        public static CreateServiceViewModel ConvertToCreateServiceViewModel(this DomainService service)
        {
            CreateServiceViewModel item = new CreateServiceViewModel()
            {
                ID = service.ID,
                ID_Executor = service.ID_Executor,
                ID_Subcategory = service.Subcategory.ID,
                ID_Measure = service.Measure.ID,
                Address = service.Address,
                Name = service.Name,
                Expirience = service.Expirience,
                Notation = service.Notation,
                ID_Place = service.Place.ID,
                Time_work = service.Time_work,
                Price = service.Price
            };
            return item;
        }

        public static ServiceViewModel ConvertToServiceViewModel(this DomainService service)
        {
            ServiceViewModel item = new ServiceViewModel()
            {
                ID = service.ID,
                ID_Executor = service.ID_Executor,                 
                Name = service.Name,             
                Time_work = service.Time_work,   
                Expirience = service.Expirience, 
                Price = service.Price,           
                Notation = service.Notation,     
                Address = service.Address,
                NamePlace = service.Place.Name,
                NameMeasure = service.Measure.Name,
                NameSubcategory = service.Subcategory.Name,
                NameCategory = service.Category.Name,
                ID_Category = service.Category.ID
            };
            return item;
        }
        public static List<ServiceViewModel> ConvertToServiceViewModel(this List<DomainService> service)
        {
            var list = new List<ServiceViewModel>();
            foreach (var item in service)
            {
                list.Add(ConvertToServiceViewModel(item));
            }
            return list;
        }
        public static DomainService ConvertToServiceDomainModel(this Service service, DomainMeasure measure, DomainPlace place, DomainSubcategory subcategory, DomainCategory category)
        {
            DomainService item = new DomainService()
            {
                ID = service.ID,
                Name = service.Name,
                Time_work = service.Time_work,
                Expirience = service.Expirience,
                Price = service.Price,
                Notation = service.Notation,
                Address = service.Address,
                Subcategory = subcategory,
                Place = place,
                ID_Executor = service.ID_Executor,
                Measure = measure,
                Category = category
            };
            return item;
        }
    }
}