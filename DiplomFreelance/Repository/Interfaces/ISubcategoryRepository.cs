using DiplomFreelance.Models.FreelanceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomFreelance.Models.Repository.Interfaces
{
    public interface ISubcategoryRepository
    {
        IEnumerable<Subcategory> GetAllSubcategory();
        Subcategory GetSubcategoryById(int id);
        void CreateSubcategory(Subcategory item); // создание объекта
        void UpdateSubcategory(Subcategory item); // обновление объекта
        void DeleteSubcategory(int id); // удаление объекта по id
        IEnumerable<Subcategory> GetSubcategoryByCategoryId(int id);
    }
}
