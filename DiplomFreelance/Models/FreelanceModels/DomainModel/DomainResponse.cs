 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels
{
    public class DomainResponse
    {
        public int ID { get; set; }
        public decimal Price { get; set; }
        public string Notation { get; set; }
        public Guid ID_Order { get; set; }
        public DomainExecutor Executor { get; set; }
        public DateTime Date { get; set; }
    }
}