using DiplomFreelance.Models.FreelanceModels;
using DiplomFreelance.Models.FreelanceModels.DBModel;
using DiplomFreelance.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.Repository
{
    public class OfferRepository : IOfferRepository
    {
        private string _connectString;
        private DataConnection _db;
        public OfferRepository(string connectionString)
        {
            _connectString = connectionString;
            _db = new DataConnection(connectionString);
        }
        public void CreateOffer(Offer item)
        {
            _db.ExecuteNonQuery($@"INSERT INTO Offer(ID_Executor, ID_Order) VALUES(N'{item.ID_Executor}', N'{item.ID_Order}')");
        }

        public void DeleteOffer(int id)
        {
            _db.ExecuteNonQuery($@"DELETE FROM Offer WHERE Offer.ID = {id}");
        }

        public IEnumerable<Offer> GetAllOffer()
        {
            return _db.ToList("SELECT * FROM Offer", Mapper.MapOffers);
        }

        public List<Offer> GetOfferByExecutorId(string idExecutor)
        {
            return _db.ToObject($"SELECT * FROM Offer Where Offer.ID_Executor = '{idExecutor}'", Mapper.MapOffers);
        }

        public Offer GetOfferById(int id)
        {
            return _db.ToObject($"SELECT * FROM Offer Where Offer.ID = {id}", Mapper.MapOffer);
        }

        public Offer GetOfferByOrderId(Guid id)
        {
            return _db.ToObject($"SELECT * FROM Offer Where Offer.ID_Order = '{id}'", Mapper.MapOffer);
        }
    }
}