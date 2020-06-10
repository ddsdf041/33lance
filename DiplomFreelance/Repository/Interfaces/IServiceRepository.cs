using DiplomFreelance.Models.FreelanceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomFreelance.Models.Repository.Interfaces
{
    public interface IServiceRepository
    {
        IEnumerable<Service> GetAllService();
        Service GetServiceById(int id);
        void CreateService(Service item); // создание объекта
        void UpdateService(Service item); // обновление объекта
        void DeleteService(int id); // удаление объекта по id
        IEnumerable<Service> GetServiceByExecutorID(string idexec);
    }
}
