using DiplomFreelance.Models.FreelanceModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.Convertors
{
    public static class CategoryConvertor
    {

        public static Category ConvertFromViewModelToDBModel(this CategoryViewModel category)
        {
            Category item = new Category()
            {
                ID = category.ID,
                Name = category.Name
            };
            return item;
        }

        public static CategoryViewModel ConvertToCategoryViewModel(this DomainCategory category)
        {
            CategoryViewModel item = new CategoryViewModel()
            {
                ID = category.ID,
                Name = category.Name,
                Subcategories = category.Subcategories.ConvertToSubcategoryViewModel()
            };
            return item;
        }
        public static List<CategoryViewModel> ConvertToCategoryViewModel(this List<DomainCategory> category)
        {
            var list = new List<CategoryViewModel>();
            foreach (var item in category)
            {
                list.Add(ConvertToCategoryViewModel(item));
            }
            return list;
        }
        public static DomainCategory ConvertToCategoryDomainModel(this Category category, List<DomainSubcategory> subcategories)
        {
            DomainCategory item = new DomainCategory()
            {
                ID = category.ID,
                Name = category.Name,
                Subcategories = subcategories
            };
            return item;
        }
    }
}