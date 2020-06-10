using DiplomFreelance.Models.FreelanceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomFreelance.Models.Repository.Interfaces
{
     public interface IResponseRepository 
    {
        IEnumerable<Response> GetAllResponse();
        Response GetResponseById(int id);
        void CreateResponse(Response item); // создание объекта
        void UpdateResponse(Response item); // обновление объекта
        void DeleteResponse(int id); // удаление объекта по id
        IEnumerable<Response> GetResponseByExecutorId(string idExecutor);
        IEnumerable<Response> GetResponseByOrderId(Guid idOrder);
        Response GetResponseByOrderIdAndExecutorId(Guid idOrder, string idExecutor);
    }
}
