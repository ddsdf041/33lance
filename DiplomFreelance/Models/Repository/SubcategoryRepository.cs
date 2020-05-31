using DiplomFreelance.Models.FreelanceModels;
using DiplomFreelance.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.Repository
{
    public class SubcategoryRepository : ISubcategoryRepository
    {
        //static readonly string connectionString = ConfigurationManager.ConnectionStrings["FreelanceConnection"].ConnectionString;
        private string _connectString;
        private DataConnection _db;
        public SubcategoryRepository(string connectionString)
        {
            _connectString = connectionString;
            _db = new DataConnection(_connectString);
        }

        public void CreateSubcategory(Subcategory item)
        {
            _db.ExecuteNonQuery($@"INSERT INTO Subcategory(ID_Category, Name) VALUES({item.ID_Category}, N'{item.Name}')");
            
        }
        public void DeleteSubcategory(int id)
        {
            _db.ExecuteNonQuery($@"DELETE FROM Subcategory WHERE Subcategory.ID = {id}");
        }
        public IEnumerable<Subcategory> GetAllSubcategory()
        {
            return _db.ToList("SELECT * FROM Subcategory", Mapper.MapSubcategories);
        }
        public Subcategory GetSubcategoryById(int id)
        {
            return _db.ToObject($"SELECT * FROM Subcategory WHERE Subcategory.ID = {id}", Mapper.MapSubcategory);
        }
        public IEnumerable<Subcategory> GetSubcategoryByCategoryId(int id)
        {
            return _db.ToList($"SELECT * FROM Subcategory WHERE Subcategory.ID_Category = {id}", Mapper.MapSubcategories);
        }
        public void UpdateSubcategory(Subcategory item)
        {
            _db.ExecuteNonQuery($@"UPDATE Subcategory SET Name = N'{item.Name}' , ID_Category = {item.ID_Category} WHERE ID = {item.ID}");
        }
    }
}