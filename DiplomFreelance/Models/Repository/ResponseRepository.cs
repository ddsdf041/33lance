using DiplomFreelance.Models.FreelanceModels;
using DiplomFreelance.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.Repository
{
    public class ResponseRepository: IResponseRepository
    {
        private string _connectString;
        private DataConnection _db;
        public ResponseRepository(string connectionString)
        {
            _connectString = connectionString;
            _db = new DataConnection(_connectString);
        }

        //IRepository
        public void CreateResponse(Response item)
        {
            string date = item.Date.ToString("yyyy-MM-dd hh:mm");
            _db.ExecuteNonQuery($@"INSERT INTO Response(ID_Executor, ID_Order, Price, Notation, Date) VALUES(N'{item.ID_Executor}', 
                                                                                                             N'{item.ID_Order}', 
                                                                                                             {item.Price}, 
                                                                                                             N'{item.Notation}', 
                                                                                                             N'{date}')");
            
        }
        public void DeleteResponse(int id)
        {
            _db.ExecuteNonQuery($@"DELETE FROM Response WHERE Response.ID = {id}");
            
        }
        public IEnumerable<Response> GetAllResponse()
        {
            return _db.ToList("SELECT * FROM Response", Mapper.MapResponses);
        }
        public Response GetResponseById(int id)
        {
            return _db.ToObject($"SELECT * FROM Response WHERE Response.ID = {id}", Mapper.MapResponse);
        }
        public void UpdateResponse(Response item)
        {
            _db.ExecuteNonQuery($@"UPDATE Response SET 
                                           Notation = '{item.Notation}',
                                           Price = {item.Price}
                                           WHERE ID = {item.ID}");
            
        }

        public IEnumerable<Response> GetResponseByExecutorId(string idExecutor)
        {
            return _db.ToList($"SELECT * FROM Response WHERE Response.ID_Executor = '{idExecutor}'", Mapper.MapResponses);
        }

        public IEnumerable<Response> GetResponseByOrderId(Guid idOrder)
        {
            return _db.ToList($"SELECT * FROM Response WHERE Response.ID_Order = '{idOrder}'", Mapper.MapResponses);
        }

        public Response GetResponseByOrderIdAndExecutorId(Guid idOrder, string idExecutor)
        {
            return _db.ToObject($"SELECT * FROM Response WHERE Response.ID_Order = '{idOrder}' and Response.ID_Executor = '{idExecutor}'", Mapper.MapResponse);
        }
    }
}