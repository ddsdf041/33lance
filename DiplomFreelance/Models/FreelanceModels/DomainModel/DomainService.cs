using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels
{
    public class DomainService
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Time_work { get; set; }
        public string Expirience { get; set; }
        public decimal Price { get; set; }
        public string Notation { get; set; }
        public string ID_Executor { get; set; }
        public string Address { get; set; }
        public DomainMeasure Measure { get; set; }
        public DomainPlace Place { get; set; }
        public DomainSubcategory Subcategory { get; set; }
        public DomainCategory Category { get; set; }

    }
}