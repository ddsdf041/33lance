using DiplomFreelance.Models.FreelanceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Controllers.BusinessLogic.IService
{
    public interface IServicePlace
    {
        DomainPlace GetPlace(int id);
            List<DomainPlace> GetAllPlace();
    }
}