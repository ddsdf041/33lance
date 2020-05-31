
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels
{
    public class DomainCategory
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public List<DomainSubcategory> Subcategories { get; set; }
    }
}