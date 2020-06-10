using DiplomFreelance.Models.FreelanceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomFreelance.Models.Repository.Interfaces
{
    public interface IPlaceRepository 
    {
        IEnumerable<Place> GetAllPlace();
        Place GetPlaceById(int id);
        void CreatePlace(Place item); // создание объекта
        void UpdatePlace(Place item); // обновление объекта
        void DeletePlace(int id); // удаление объекта по id
    }
}