using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.ViewModel
{
    public class ServiceViewModel
    {
        public int ID { get; set; }
        public string ID_Executor { get; set; } // <--- make it
        public string Name { get; set; }
        public string Time_work { get; set; }
        public string Expirience { get; set; }
        public decimal Price { get; set; }
        public string Notation { get; set; }
        public string Address { get; set; }

        public int ID_Category { get; set; }
        public string NameMeasure { get; set; } // <--- make it
        public string NamePlace { get; set; } // <--- make it
        public string NameSubcategory { get; set; } // <--- make it
        public string NameCategory { get; set; } // <--- make it
    }
}