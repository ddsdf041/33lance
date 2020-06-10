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
    public class CategoryRepository : ICategoryRepository
    {
        private string _connectString;
        private DataConnection _db;
        public CategoryRepository(string connectionString)
        {
            _connectString = connectionString;
            _db = new DataConnection(connectionString);
        }
        public Category GetCategoryBySubcategoryId(int id)
        {
                return _db.ToObject($"SELECT Category.ID, Category.Name FROM Category JOIN Subcategory on (Subcategory.ID_Category = Category.ID) WHERE Subcategory.ID = {id}", Mapper.MapCategory);
        }
        //IRepository
        public void CreateCategory(Category item)
        {
            _db.ExecuteNonQuery($@"INSERT INTO Category(Name) VALUES(N'{item.Name}')");
            
        }
        public void DeleteCategory(int id)
        {
            _db.ExecuteNonQuery($@"DELETE FROM Category WHERE Category.ID = {id}");
            
        }
        public IEnumerable<Category> GetAllCategory()
        {
                return _db.ToList("SELECT * FROM Category", Mapper.MapCategories);
        }
        public Category GetCategoryById(int id)
        {
                return _db.ToObject($"SELECT * FROM Category WHERE Category.ID = {id}", Mapper.MapCategory);
        }
        public void UpdateCategory(Category item)
        {
            _db.ExecuteNonQuery($@"UPDATE Category SET Name = N'{item.Name}' WHERE ID = {item.ID}");
        }

    }
}