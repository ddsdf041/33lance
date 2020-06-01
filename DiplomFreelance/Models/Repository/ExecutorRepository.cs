using DiplomFreelance.Models.FreelanceModels;
using DiplomFreelance.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.Repository
{
    public class ExecutorRepository : IExecutorRepository
    { 
        private string _connectString;
        private DataConnection _db;
        public ExecutorRepository(string connectionString)
        {
            _connectString = connectionString;
            _db = new DataConnection(_connectString);
        }

        public IEnumerable<Executor> GetExecutorsBySubId(int IdSubcategory)
        {
            return _db.ToList($@"Select 
                              Executor.ID_User,
                              Executor.Name,
                              Executor.Email,
                              Executor.Description,
                              Executor.Photo,
                              Executor.Raiting,
                              Executor.Speciality,
                              Executor.City,
                              Executor.Telephone,
                              Executor.IsBanned from Executor
                              join Service on(Service.ID_Executor = Executor.ID_User)
                              join Subcategory on(Subcategory.ID = Service.ID_Subcategory)
                              where Subcategory.ID = {IdSubcategory} 
                              GROUP BY
                              Executor.ID_User,
                              Executor.Name,
                              Executor.Email,
                              Executor.Description,
                              Executor.Photo,
                              Executor.Raiting,
                              Executor.Speciality,
                              Executor.City,
                              Executor.Telephone,
                              Executor.IsBanned", Mapper.MapExecutors);

        }
        public IEnumerable<Executor> GetExecutorsByServiceName(string nameSubcategory)
        {
            return _db.ToList($@"Select 
                              Executor.ID_User,
                              Executor.Name,
                              Executor.Email,
                              Executor.Description,
                              Executor.Photo,
                              Executor.Raiting,
                              Executor.Speciality,
                              Executor.City,
                              Executor.Telephone,
                              Executor.IsBanned
                              from Executor
                              join Service on(Service.ID_Executor = Executor.ID_User)
                              join Subcategory on(Subcategory.ID = Service.ID_Subcategory)
                              where Subcategory.Name LIKE N'%{nameSubcategory}%' or Subcategory.Name LIKE N'%сайт%'  
                              GROUP BY
                              Executor.ID_User,
                              Executor.Name,
                              Executor.Email,
                              Executor.Description,
                              Executor.Photo,
                              Executor.Raiting,
                              Executor.Speciality,
                              Executor.City,
                              Executor.Telephone,
                              Executor.IsBanned", Mapper.MapExecutors);
        }

        //IRepository
        public void CreateExecutor(Executor item)
        {
            _db.ExecuteNonQuery($@"INSERT INTO Executor(ID_User, Name, Email, Specialty, Telephone, Raiting, Description, Photo, City, IsBanned) 
                               VALUES(N'{item.ID_User}', N'{item.Name}', N'{item.Email}', N'{item.Specialty}', N'{item.Telephone}', {item.Raiting}, N'{item.Description}', N'{item.Photo}', N'{item.City}', 'false')");
            
        }
        public void DeleteExecutor(string id)
        {
            _db.ExecuteNonQuery($@"DELETE FROM Executor WHERE Executor.ID_User = '{id}'");
            
        }
        public IEnumerable<Executor> GetAllExecutor()
        {
            return _db.ToList("SELECT * FROM Executor", Mapper.MapExecutors);
        }
        public Executor GetByUserId(string id_User)
        {
            return _db.ToObject($"SELECT * FROM Executor WHERE Executor.ID_User = '{id_User}'", Mapper.MapExecutor);
        }
        public Executor GetExecutirByEmail(string email)
        {
            return _db.ToObject($"SELECT * FROM Executor WHERE Executor.Email = '{email}'", Mapper.MapExecutor);
        }
        public void UpdateExecutor(Executor item)
        {
            _db.ExecuteNonQuery($@"UPDATE Executor SET 
                           Executor.Name=N'{item.Name}', 
                           Executor.Speciality=N'{item.Specialty}', 
                           Executor.Telephone=N'{item.Telephone}', 
                           Executor.Description=N'{item.Description}', 
                           Executor.Photo=N'{item.Photo}', 
                           Executor.City=N'{item.City}' 
                           WHERE Executor.ID_User = N'{item.ID_User}'");

        }

        public void BannedExecutor(string id)
        {
            _db.ExecuteNonQuery($"UPDATE Executor SET IsBanned = 'true' WHERE Executor.ID_User = '{id}'");
        }

        public void UnBannedExecutor(string id)
        {
            _db.ExecuteNonQuery($"UPDATE Executor SET IsBanned = 'false' WHERE Executor.ID_User = '{id}'");
        }
    }
}