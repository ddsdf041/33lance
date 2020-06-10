using DiplomFreelance.Models.FreelanceModels;
using DiplomFreelance.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.Repository
{
    public class StatusRepository : IStatusRepository
    {
        private string _connectString;
        private DataConnection _db;
        public StatusRepository(string connectionString)
        {
            _connectString = connectionString;
            _db = new DataConnection(_connectString);
        }

        //IRepository
        public void CreateStatus(Status item)
        {
            _db.ExecuteNonQuery($@"INSERT INTO Status(Name) VALUES(N'{item.Name}')");
           
        }
        public void DeleteStatus(int id)
        {
            _db.ExecuteNonQuery($@"DELETE FROM Status WHERE Status.ID = {id}");
            
        }
        public IEnumerable<Status> GetAllStatus()
        {
            return _db.ToList("SELECT * FROM Status", Mapper.MapStatuses);
        }
        public Status GetStatusById(int id)
        {
            return _db.ToObject($"SELECT * FROM Status WHERE Status.ID = {id}", Mapper.MapStatus);
        }
        public void UpdateStatus(Status item)
        {
            _db.ExecuteNonQuery($@"UPDATE Status SET Name = N'{item.Name}' WHERE ID = {item.ID}");
        }
    }
}