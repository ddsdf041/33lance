using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.DomainModel
{
    public class DomainOffer
    {
        public int ID { get; set; }
        public Guid ID_Order { get; set; }
        public string ID_Executor { get; set; }

    }
}