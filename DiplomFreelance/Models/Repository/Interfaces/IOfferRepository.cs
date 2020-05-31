
using DiplomFreelance.Models.FreelanceModels.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.Repository.Interfaces
{
    public interface IOfferRepository
    {
        IEnumerable<Offer> GetAllOffer();
        Offer GetOfferById(int id);
        void CreateOffer(Offer item); // создание объекта
        void DeleteOffer(int id); // удаление объекта по id
        List<Offer> GetOfferByExecutorId(string id);
        Offer GetOfferByOrderId(Guid id);

    }
}