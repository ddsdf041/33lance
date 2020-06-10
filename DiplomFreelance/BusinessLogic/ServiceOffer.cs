using DiplomFreelance.Controllers.BusinessLogic.IService;
using DiplomFreelance.Models.FreelanceModels.Convertors;
using DiplomFreelance.Models.FreelanceModels.DBModel;
using DiplomFreelance.Models.FreelanceModels.DomainModel;
using DiplomFreelance.Models.FreelanceModels.ViewModel;
using DiplomFreelance.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Controllers.BusinessLogic
{
    public class ServiceOffer : IServiceOffer
    {
        private IOfferRepository _offerRepository;
        //private IServiceOrder _serviceOrder;

        public ServiceOffer(IOfferRepository offerRepository/*, IServiceOrder serviceOrder*/)
        {
            _offerRepository = offerRepository;
            //_serviceOrder = serviceOrder;
        }

        private DomainOffer GetOffer(Offer offer)
        {
            //var order = _serviceOrder.GetOrderById(offer.ID_Order);
            return offer.ConvertToCustomerDomainModel(/*order*/);
        }

        private List<DomainOffer> GetOffers(List<Offer> offer)
        {
            var offers = new List<DomainOffer> { };
            foreach (var item in offer)
            {
                offers.Add(GetOffer(item));
            }
            return offers;
        }

        public DomainOffer GetOfferById(int id)
        {
            return GetOffer(_offerRepository.GetOfferById(id));
        }
        public DomainOffer GetOfferByOrderId(Guid id)
        {
            return GetOffer(_offerRepository.GetOfferByOrderId(id));
        }
        public List<DomainOffer> GetOffersByExecutorId(string idExecutor)
        {
            return GetOffers(_offerRepository.GetOfferByExecutorId(idExecutor));
        }
        
        public bool CreateNewOffer(OfferViewModel item)
        {
            try
            {
                _offerRepository.CreateOffer(item.ConvertFromViewModelToDBModel());
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public bool DeleteOffer(int id)
        {
            try
            {
                _offerRepository.DeleteOffer(id);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}