using DiplomFreelance.Models.FreelanceModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.Convertors
{
    public static class StatusConvertor
    {
        public static StatusViewModel ConvertToStatusViewModel(this DomainStatus status)
        {
            StatusViewModel item = new StatusViewModel()
            {
                ID = status.ID,
                Name = status.Name
            };
            return item;
        }
        public static DomainStatus ConvertToStatusDomainModel(this Status status)
        {
            DomainStatus item = new DomainStatus()
            {
                ID = status.ID,
                Name = status.Name
            };
            return item;
        }
    }
}