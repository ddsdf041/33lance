using DiplomFreelance.Models.FreelanceModels.DBModel;
using DiplomFreelance.Models.FreelanceModels.DomainModel;
using DiplomFreelance.Models.FreelanceModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.Convertors
{
    public static class OfferConvertor
    {
        public static Offer ConvertFromViewModelToDBModel(this OfferViewModel Offer)
        {
            Offer item = new Offer()
            {
                ID = Offer.ID,
                ID_Executor = Offer.ID_Executor,
                ID_Order = Offer.ID_Order
            };
            return item;
        }
        public static OfferViewModel ConvertToOfferViewModel(this DomainOffer Offer)
        {
            OfferViewModel item = new OfferViewModel()
            {
                ID = Offer.ID,
                ID_Executor = Offer.ID_Executor,
                ID_Order = Offer.ID_Order
            };
            return item;
        }
        public static List<OfferViewModel> ConvertToCategoryViewModel(this List<DomainOffer> Offer)
        {
            var list = new List<OfferViewModel>();
            foreach (var item in Offer)
            {
                list.Add(ConvertToOfferViewModel(item));
            }
            return list;
        }
        public static DomainOffer ConvertToCustomerDomainModel(this Offer offer/*, DomainOrder order*/)
        {

            DomainOffer item = new DomainOffer()
            {
                ID = offer.ID,
                ID_Executor = offer.ID_Executor,
                ID_Order = offer.ID_Order
            };
            return item;
        }

    }
}