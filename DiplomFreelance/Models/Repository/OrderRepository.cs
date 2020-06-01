using DiplomFreelance.Models.FreelanceModels;
using DiplomFreelance.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private string _connectString;
        private DataConnection _db;
        //static readonly string connectionString = ConfigurationManager.ConnectionStrings["FreelanceConnection"].ConnectionString;
        public OrderRepository(string connectionString)
        {
            _connectString = connectionString;
            _db = new DataConnection(_connectString);
        }

        public IEnumerable<Order> GetOrderByExecutorId(string idexec)
        {
            return _db.ToList($"SELECT * FROM [Order] WHERE [Order].ID_Executor = '{idexec}'", Mapper.MapOrders);
        }
        public IEnumerable<Order> GetOrderBySubcategoryId(int idSubcategory)
        {
            return _db.ToList($"SELECT * FROM [Order] WHERE [Order].ID_Subcategory = {idSubcategory}", Mapper.MapOrders);
        }
        public IEnumerable<Order> GetOrderByCustomerId(string idcustomer)
        {
            return _db.ToList($"SELECT * FROM [Order] WHERE [Order].ID_Customer = '{idcustomer}'", Mapper.MapOrders);
        }
        //IRepository
        public void CreateOrder(Order item)
        {
            var parameters = new SqlParameter[1];
           
            SqlParameter address = new SqlParameter("@address", item.Address);
            address.Value = address.Value ?? DBNull.Value;
            parameters[0] = address;
            string deadline = item.Deadline.ToString("yyyy-MM-dd");
            TimeZoneInfo moscowTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time");
            string moscowTime = (DateTime.UtcNow + moscowTimeZone.BaseUtcOffset).ToString("yyyy-MM-dd HH:mm");           

            _db.ExecuteNonQuery($@"INSERT INTO [Order](ID,Name,ID_Customer,ID_Status,ID_Executor,ID_Subcategory,Description,Deadline,ID_Measure,Budget,ID_Place,Address,Date,IsBanned) 
                                        VALUES(N'{item.ID}',
                                               N'{item.Name}',
                                               N'{item.ID_Customer}',
                                               {item.ID_Status},
                                                null,
                                               {item.ID_Subcategory},
                                             N'{item.Description}',
                                              N'{deadline}',
                                               {item.ID_Measure},
                                               {item.Budget},
                                               {item.ID_Place},
                                                @address,
                                               N'{moscowTime}',
                                               'false')", parameters);
            /*SELECT LAST_INSERT_ID();*/

        }
        public void DeleteOrder(Guid id)
        {
            _db.ExecuteNonQuery($@"DELETE FROM [Order] WHERE [Order].ID = '{id}'");
            
        }
        public IEnumerable<Order> GetAllOrder()
        {
            return _db.ToList("SELECT * FROM [Order]", Mapper.MapOrders);
        }
        public IEnumerable<Order> GetAllOrderWhereExecutorIsNull()
        {
            return _db.ToList("SELECT * FROM [Order] WHERE ID_Executor is null and ID_Status = 1 and IsBanned = 'false'", Mapper.MapOrders);
        }
        public IEnumerable<Order> GetAllOrderWhenOfferedToExecutor(string idExecutor)
        {
            return _db.ToList($@"SELECT * FROM [Order] join Offer on ([Order].ID = Offer.ID_Order)
                                 where Offer.ID_Executor = '{idExecutor}' and [Order].ID_Status = 4 and IsBanned = 'false'", Mapper.MapOrders);
        }
        public Order GetOrderById(Guid id)
        {
            return _db.ToObject($"SELECT * FROM [Order] WHERE [Order].ID = '{id}'", Mapper.MapOrder);
        }

        public void UpdateStatusOrder(Guid idOrder, int status)
        {
            _db.ExecuteNonQuery($@"UPDATE [Order] SET ID_Status = {status} WHERE ID = '{idOrder}'");
        }

        public void UpdateExecutorOrder(string idExecutor, Guid idOrder, decimal budget)
        {
            _db.ExecuteNonQuery($@"UPDATE [Order] SET ID_Executor = '{idExecutor}', ID_Status = 2, Budget = {Math.Round(budget)} WHERE [Order].ID = '{idOrder}'");
            
        }

        public void UpdateExecutorOrder(string idExecutor, Guid idOrder)
        {
            _db.ExecuteNonQuery($@"UPDATE [Order] SET ID_Executor = '{idExecutor}', ID_Status = 2 WHERE [Order].ID = '{idOrder}'");

        }
        public void BannedOrder(Guid id)
        {
            _db.ExecuteNonQuery($"UPDATE [Order] SET IsBanned = 'true' WHERE [Order].ID = '{id}'");
        }

        public void UnBannedOrder(Guid id)
        {
            _db.ExecuteNonQuery($"UPDATE [Order] SET IsBanned = 'false' WHERE [Order].ID = '{id}'");
        }

    }
}