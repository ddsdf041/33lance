using DiplomFreelance.Models.FreelanceModels.DomainModel;
using DiplomFreelance.Models.FreelanceModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Controllers.BusinessLogic.IService
{
    public interface IServiceOffer
    {
        List<DomainOffer> GetOffersByExecutorId(string idExecutor);
        DomainOffer GetOfferById(int id);
        bool CreateNewOffer(OfferViewModel item);
        DomainOffer GetOfferByOrderId(Guid id);
        bool DeleteOffer(int id);
    }
}