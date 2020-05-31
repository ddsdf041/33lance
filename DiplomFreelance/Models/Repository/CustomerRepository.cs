using DiplomFreelance.Models.FreelanceModels;
using DiplomFreelance.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.Repository
{
    public class CustomerRepository: ICustomerRepository
    {
        private string _connectString;
        private DataConnection _db;
        //static readonly string connectionString = ConfigurationManager.ConnectionStrings["FreelanceConnection"].ConnectionString;
        public CustomerRepository(string connectionString)
        {
            _connectString = connectionString;
            _db = new DataConnection(connectionString);
        }

        //IRepository
        public void CreateCustomer(Customer item)
        {
            _db.ExecuteNonQuery($@"INSERT INTO Customer(Name,ID_User,Email, IsBanned) VALUES(N'{item.Name}',N'{item.ID_User}',N'{item.Email}', 'false')");
        }
        public void DeleteCustomer(string id_User)
        {
            _db.ExecuteNonQuery($@"DELETE FROM Customer WHERE Customer.ID_User = '{id_User}'");
        }
        public IEnumerable<Customer> GetAllCustomer()
        {
            return _db.ToList("SELECT * FROM Customer", Mapper.MapCustomers);
        }
        public Customer GetCustomerByUserId(string id_User)
        {
            return _db.ToObject($"SELECT * FROM Customer WHERE Customer.ID_User = '{id_User}'", Mapper.MapCustomer);
        }
        public Customer GetCustomerByEmail(string email)
        {
            return _db.ToObject($"SELECT * FROM Customer WHERE Customer.Email = '{email}'", Mapper.MapCustomer);
        }
        public void UpdateCustomer(Customer item)
        {
            _db.ExecuteNonQuery($@"UPDATE Customer SET Name = N'{item.Name}', Email = N'{item.Email}' WHERE ID_User = '{item.ID_User}'");
        }
        public void BannedCustomer(string id)
        {
            _db.ExecuteNonQuery($"UPDATE Customer SET IsBanned = 'true' WHERE Customer.ID_User = '{id}';");
           _db.ExecuteNonQuery($"UPDATE [Order] SET IsBanned = 'true' WHERE [Order].ID_Customer = '{id}'");
        }

        public void UnBannedCustomer(string id)
        {
            _db.ExecuteNonQuery($"UPDATE Customer SET IsBanned = 'false' WHERE Customer.ID_User = '{id}';");
             _db.ExecuteNonQuery($"UPDATE [Order] SET IsBanned = 'false' WHERE [Order].ID_Customer = '{id}'");
        }
    }
}