using DiplomFreelance.Models.FreelanceModels;
using DiplomFreelance.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomFreelance.Controllers.BusinessLogic.IService
{
    public interface IServiceCategory
    {
        List<DomainCategory> GetCategories();
        DomainCategory GetCategoryBySubcategoryId(int idSub);

        DomainCategory GetCategoryById(int id);
        bool UpdateCategory(Category item);
        bool DeleteCategory(int id);
        bool CreateCategory(Category item);
    }
}
