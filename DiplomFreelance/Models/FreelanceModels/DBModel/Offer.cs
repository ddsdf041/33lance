using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.DBModel
{
    public class Offer
    {
        public int ID { get; set; }
        public Guid ID_Order { get; set; }
        public string ID_Executor { get; set; }

    }
}