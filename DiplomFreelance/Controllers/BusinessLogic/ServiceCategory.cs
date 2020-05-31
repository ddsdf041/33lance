using DiplomFreelance.Controllers.BusinessLogic.IService;
using DiplomFreelance.Models.FreelanceModels;
using DiplomFreelance.Models.FreelanceModels.Convertors;
using DiplomFreelance.Models.Repository;
using DiplomFreelance.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Controllers
{
    public class ServiceCategory: IServiceCategory
    {
        private ICategoryRepository _categoryRepository;
        private IServiceSubcategory _serviceSubcategory;

        public ServiceCategory(IServiceSubcategory serviceSubcategory, 
                               ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _serviceSubcategory = serviceSubcategory;
        }

        private DomainCategory GetCategory(Category category)
        {
            var subcategories = _serviceSubcategory.GetSubcategories(category.ID);
            return category.ConvertToCategoryDomainModel(subcategories);
        }
        public List<DomainCategory> GetCategories()
        {
            var categories = new List<DomainCategory> { };

            foreach (var item in _categoryRepository.GetAllCategory())
            {
                categories.Add(GetCategory(item));
            }
            return categories;
        }

        public DomainCategory GetCategoryBySubcategoryId(int idSub)
        {
            return GetCategory(_categoryRepository.GetCategoryBySubcategoryId(idSub));
        }

        public DomainCategory GetCategoryById(int id)
        {
            return GetCategory(_categoryRepository.GetCategoryById(id));
        }
        public bool CreateCategory(Category item)
        {
            try
            {
                _categoryRepository.CreateCategory(item);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool UpdateCategory(Category item)
        {
            try
            {
                _categoryRepository.UpdateCategory(item);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool DeleteCategory(int id)
        {
            try
            {
                _categoryRepository.DeleteCategory(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}