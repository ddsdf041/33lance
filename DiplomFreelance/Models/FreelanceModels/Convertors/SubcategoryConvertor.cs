using DiplomFreelance.Models.FreelanceModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.Convertors
{
    public static class SubcategoryConvertor
    {
        public static Subcategory ConvertFromViewModelToDBModel(this SubcategoryViewModel subcategory)
        {
            Subcategory item = new Subcategory()
            {
                ID = subcategory.ID,
                Name = subcategory.Name,
                ID_Category = subcategory.Category.ID
            };
            return item;
        }
        public static SubcategoryViewModel ConvertToSubcategoryViewModel(this DomainSubcategory subcategory)
        {
            SubcategoryViewModel item = new SubcategoryViewModel()
            {
                ID = subcategory.ID,
                Name = subcategory.Name
            };
            return item;
        }
        public static List<SubcategoryViewModel> ConvertToSubcategoryViewModel(this List<DomainSubcategory> subcategory)
        {
            var list = new List<SubcategoryViewModel>();
            foreach (var itemSubCategory in subcategory)
            {
                list.Add(ConvertToSubcategoryViewModel(itemSubCategory));
            }
            return list;
        }

        public static DomainSubcategory ConvertToSubcategoryDomainModel(this Subcategory subcategory)
        {
            DomainSubcategory item = new DomainSubcategory()
            {
                ID = subcategory.ID,
                Name = subcategory.Name,
                ID_Category = subcategory.ID_Category
            };
            return item;
        }

        public static List<DomainSubcategory> ConvertToSubcategoryDomainModel(this List<Subcategory> subcategory)
        {
            var list = new List<DomainSubcategory>();
            foreach (var item in subcategory)
            {
                list.Add(ConvertToSubcategoryDomainModel(item));
            }
            return list;
        }
    }
}