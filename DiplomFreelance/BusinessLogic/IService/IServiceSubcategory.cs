using DiplomFreelance.Models.FreelanceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomFreelance.Controllers.BusinessLogic.IService
{
    public interface IServiceSubcategory
    {
        List<DomainSubcategory> GetSubcategories(int category);
        DomainSubcategory GetSubcategoryById(int idSub);
        bool UpdateSubcategory(Subcategory item);
        bool CreateSubcategory(Subcategory item);
        bool DeleteSubcategory(int id);
    }
}
