using DiplomFreelance.Models.FreelanceModels;
using DiplomFreelance.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.Repository
{
    public class FileOrderRepository : IFileOrderRepository
    {
        private string _connectString;
        private DataConnection _db;
        public FileOrderRepository(string connectionString)
        {
            _connectString = connectionString;
            _db = new DataConnection(_connectString);
        }

        //IRepository
        public void CreateFile(FileOrder item)
        {
            _db.ExecuteNonQuery($@"INSERT INTO FileOrder(Name, Image, ID_Order) VALUES(N'{item.Name}', N'{item.Image}', N'{item.ID_Order}')");
        }
        public void DeleteFile(int id)
        {
            _db.ExecuteNonQuery($@"DELETE FROM FileOrder WHERE FileOrder.ID = {id}");
        }
        public IEnumerable<FileOrder> GetAllFile()
        {
            return _db.ToList("SELECT * FROM FileOrder", Mapper.MapFilesOrder);
        }
        public FileOrder GetFileById(int id)
        {
            return _db.ToObject($"SELECT * FROM FileOrder WHERE FileOrder.ID = {id}", Mapper.MapFileOrder);
        }
        public IEnumerable<FileOrder> GetFileByOrderId(Guid id)
        {
            return _db.ToList($"SELECT * FROM FileOrder WHERE FileOrder.ID_Order = '{id}'", Mapper.MapFilesOrder);
        }
        public void UpdateFile(FileOrder item)
        {
            _db.ExecuteNonQuery($@"UPDATE FileOrder SET Name = N'{item.Name}', Image = N'{item.Image}', ID_Order = N'{item.ID_Order}' WHERE ID = {item.ID}");

        }
    }
}