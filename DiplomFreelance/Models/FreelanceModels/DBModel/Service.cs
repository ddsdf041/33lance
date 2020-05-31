using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels
{
    public class Service
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ID_Subcategory { get; set; }
        public string Time_work { get; set; }
        public int Place { get; set; }
        public string Expirience { get; set; }
        public int ID_Measure { get; set; }
        public decimal Price { get; set; }
        public string Notation { get; set; }
        public string ID_Executor { get; set; }
        public string Address { get; set; }

    }
}