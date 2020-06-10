using DiplomFreelance.Models.FreelanceModels;
using DiplomFreelance.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.Repository
{
    public class MeasureRepository : IMeasureRepository
    {
        private string _connectString;
        private DataConnection _db;
        public MeasureRepository(string connectionString)
        {
            _connectString = connectionString;
            _db = new DataConnection(_connectString);
        }

        //IRepository
        public void CreateMeasure(Measure item)
        {
            _db.ExecuteNonQuery($@"INSERT INTO Measure(Name) VALUES(N'{item.Name}')");
            
        }
        public void DeleteMeasure(int id)
        {
            _db.ExecuteNonQuery($@"DELETE FROM Measure WHERE Measure.ID = {id}");
            
        }
        public IEnumerable<Measure> GetAllMeasure()
        {
            return _db.ToList("SELECT * FROM Measure", Mapper.MapMeasures);
        }
        public Measure GetMeasureById(int id)
        {
            return _db.ToObject($"SELECT * FROM Measure WHERE Measure.ID = {id}", Mapper.MapMeasure);
        }
        public void UpdateMeasure(Measure item)
        {
            _db.ExecuteNonQuery($@"UPDATE Measure SET Name = N'{item.Name}' WHERE ID = {item.ID}");
            
        }
    }
}