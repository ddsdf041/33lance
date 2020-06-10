using DiplomFreelance.Controllers.BusinessLogic.IService;
using DiplomFreelance.Models.FreelanceModels;
using DiplomFreelance.Models.FreelanceModels.Convertors;
using DiplomFreelance.Models.FreelanceModels.ViewModel;
using DiplomFreelance.Models.Repository;
using DiplomFreelance.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Controllers
{
    public class ServiceSubcategory: IServiceSubcategory
    {
        private ISubcategoryRepository _subcategoryRepository;
        public ServiceSubcategory(ISubcategoryRepository subcategoryRepository)
        {
            _subcategoryRepository = subcategoryRepository;
        }

        public List<DomainSubcategory> GetSubcategories(int category)
        { 
            var list = _subcategoryRepository.GetSubcategoryByCategoryId(category);

            var result = SubcategoryConvertor.ConvertToSubcategoryDomainModel(list.ToList());
            return result;
        }

        public DomainSubcategory GetSubcategoryById(int idSub)
        {
            var item = _subcategoryRepository.GetSubcategoryById(idSub);
            return item.ConvertToSubcategoryDomainModel();
        }

        public bool CreateSubcategory(Subcategory item)
        {
            try
            {
                _subcategoryRepository.CreateSubcategory(item);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool UpdateSubcategory(Subcategory item)
        {
            try
            {
                _subcategoryRepository.UpdateSubcategory(item);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool DeleteSubcategory(int id)
        {
            try
            {
                _subcategoryRepository.DeleteSubcategory(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }


    }
}