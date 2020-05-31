using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.DomainModel
{
    public class DomainChat
    {
        public int ID { get; set; }
        public DomainCustomer Customer { get; set; }
        public DomainExecutor Executor { get; set; }
    }
}