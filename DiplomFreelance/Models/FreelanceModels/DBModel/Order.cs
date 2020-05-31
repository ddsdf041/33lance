using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels
{
    public class Order
    {
        public Guid ID { get; set; }
        public string ID_Customer { get; set; }
        public int ID_Status { get; set; }
        public string ID_Executor { get; set; }
        public string Name { get; set; }
        public int ID_Subcategory { get; set; }
        public string Description { get; set; }
        public System.DateTime Deadline { get; set; }
        public int ID_Measure { get; set; }
        public decimal Budget { get; set; }
        public int ID_Place { get; set; }
        public string Address { get; set; }
        public bool IsBanned { get; set; }

        public System.DateTime Date { get; set; }

        //public Customer Customer { get; set; }
        //public Status Status { get; set; }
        //public Executor Executor { get; set; }
        //public Subcategory Subcategory { get; set; }
        //public Measure Measure { get; set; }
        //public Place Place { get; set; }
    }
}