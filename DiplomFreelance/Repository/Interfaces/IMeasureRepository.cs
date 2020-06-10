using DiplomFreelance.Models.FreelanceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomFreelance.Models.Repository.Interfaces
{
    public interface IMeasureRepository 
    {
        IEnumerable<Measure> GetAllMeasure();
        Measure GetMeasureById(int id);
        void CreateMeasure(Measure item); // создание объекта
        void UpdateMeasure(Measure item); // обновление объекта??
        void DeleteMeasure(int id); // удаление объекта по id

    }
}