using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.ViewModel
{
    public class CategoryViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<SubcategoryViewModel> Subcategories { get; set; }
    }
}