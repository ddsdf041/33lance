using DiplomFreelance.Models.FreelanceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomFreelance.Models.Repository.Interfaces
{
    public interface IStatusRepository 
    {
        IEnumerable<Status> GetAllStatus();
        Status GetStatusById(int id);
        void CreateStatus(Status item); // создание объекта
        void UpdateStatus(Status item); // обновление объекта
        void DeleteStatus(int id); // удаление объекта по id
    }
}
