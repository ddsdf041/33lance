using DiplomFreelance.Models.FreelanceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.Repository.Interfaces
{
    public interface ICategoryRepository 
    {
          IEnumerable<Category> GetAllCategory();
          Category GetCategoryById(int id);
          void CreateCategory(Category item); // создание объекта
          void UpdateCategory(Category item); // обновление объекта
          void DeleteCategory(int id); // удаление объекта по id
          Category GetCategoryBySubcategoryId(int id);
    }
}