using DiplomFreelance.Models.FreelanceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomFreelance.Models.Repository.Interfaces
{
    public interface IExecutorRepository 
    {
        IEnumerable<Executor> GetAllExecutor();
        void CreateExecutor(Executor item); // создание объекта
        void UpdateExecutor(Executor item); // обновление объекта
        void DeleteExecutor(string id); // удаление объекта по id
        IEnumerable<Executor> GetExecutorsBySubId(int IdSubcategory);
        IEnumerable<Executor> GetExecutorsByServiceName(string nameSubcategory);
        Executor GetByUserId(string id_User);
        void BannedExecutor(string id);
        void UnBannedExecutor(string id);
        Executor GetExecutirByEmail(string email);
    }
}