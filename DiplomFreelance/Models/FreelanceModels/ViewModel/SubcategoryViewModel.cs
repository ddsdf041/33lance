using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.ViewModel
{
    public class SubcategoryViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public CategoryViewModel Category { get; set; }
    }
}