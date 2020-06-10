using DiplomFreelance.Models.FreelanceModels;
using DiplomFreelance.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        private string _connectString;
        private DataConnection _db;
        //static readonly string connectionString = ConfigurationManager.ConnectionStrings["FreelanceConnection"].ConnectionString;
        public ServiceRepository(string connectionString)
        {
            _connectString = connectionString;
            _db = new DataConnection(_connectString);
        }

        public IEnumerable<Service> GetServiceByExecutorID(string idexec)
        {
            return _db.ToList($"SELECT * FROM Service WHERE Service.ID_Executor = '{idexec}'", Mapper.MapServices);
        }

        public void CreateService(Service item)
        {
           
            var parameters = new SqlParameter[2]; 

            SqlParameter notation = new SqlParameter("@notation", item.Notation);
            notation.Value = notation.Value ?? DBNull.Value;
            parameters[0] = notation;
            SqlParameter address = new SqlParameter("@address", item.Address);
            address.Value = address.Value ?? DBNull.Value;
            parameters[1] = address;

            _db.ExecuteNonQuery($@"INSERT INTO [Service] (Name,
                                               ID_Subcategory,
                                               Time_work,
                                               Place,
                                               Expirience,
                                               ID_Measure,
                                               Price,
                                               Notation,
                                               ID_Executor,
                                               Address)
                                       VALUES(N'{item.Name}',
                                               {item.ID_Subcategory},
                                              N'{item.Time_work}',
                                               {item.Place},
                                               N'{item.Expirience}',
                                              {item.ID_Measure},
                                              {item.Price},
                                               @notation,
                                              N'{item.ID_Executor}',
                                               @address)", parameters);
            
        }
        public void DeleteService(int id)
        {
            _db.ExecuteNonQuery($@"DELETE FROM Service WHERE Service.ID = {id}");
            
        }
        public IEnumerable<Service> GetAllService()
        {
            return _db.ToList("SELECT * FROM Service", Mapper.MapServices);
        }
        public Service GetServiceById(int id)
        {
            return _db.ToObject($"SELECT * FROM Service WHERE Service.ID = {id}", Mapper.MapService);
        }
        public void UpdateService(Service item)
        {

            var parameters = new SqlParameter[2];
            SqlParameter exec = new SqlParameter("@notation", item.Notation);
            exec.Value = exec.Value ?? DBNull.Value;
            parameters[0]=exec;
            SqlParameter address = new SqlParameter("@address", item.Address);
            address.Value = address.Value ?? DBNull.Value;
            parameters[1] = address;

            _db.ExecuteNonQuery($@"UPDATE Service SET Name = N'{item.Name}',
                                              Time_work = N'{item.Time_work}',
                                               ID_Subcategory = {item.ID_Subcategory},
                                               Place = {item.Place},
                                               Expirience = N'{item.Expirience}',
                                               ID_Measure = {item.ID_Measure},
                                               Price = {item.Price},
                                               Notation =   @notation,
                                               ID_Executor = N'{item.ID_Executor}',
                                               Address = @address
                                              WHERE ID = {item.ID}", parameters);
            
        }
    }
}