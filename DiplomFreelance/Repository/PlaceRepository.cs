using DiplomFreelance.Models.FreelanceModels;
using DiplomFreelance.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.Repository
{
    public class PlaceRepository : IPlaceRepository
    {
        private string _connectString;
        private DataConnection _db;
        public PlaceRepository(string connectionString)
        {
            _connectString = connectionString;
            _db = new DataConnection(_connectString);
        }

        //IRepository
        public void CreatePlace(Place item)
        {
            _db.ExecuteNonQuery($@"INSERT INTO Place(Name) VALUES(N'{item.Name}')");
            
        }
        public void DeletePlace(int id)
        {
            _db.ExecuteNonQuery($@"DELETE FROM Place WHERE Place.ID = {id}");
            
        }
        public IEnumerable<Place> GetAllPlace()
        {
            return _db.ToList("SELECT * FROM Place", Mapper.MapPlaces);
        }
        public Place GetPlaceById(int id)
        {
            return _db.ToObject($"SELECT * FROM Place WHERE Place.ID = {id}", Mapper.MapPlace);
        }
        public void UpdatePlace(Place item)
        {
            _db.ExecuteNonQuery($@"UPDATE Place SET Name = N'{item.Name}' WHERE ID = {item.ID}");
            
        }
    }
}