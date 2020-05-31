using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels
{
    public class DomainOrder
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public System.DateTime Deadline { get; set; }
        public decimal Budget { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
        public bool IsBanned { get; set; }


        public DomainCustomer Customer { get; set; }
        public DomainExecutor Executor { get; set; }
        public DomainStatus Status { get; set; }
        public DomainSubcategory Subcategory { get; set; }
        public DomainCategory Category { get; set; }
        public DomainMeasure Measure { get; set; }
        public DomainPlace Place { get; set; }

        public List<DomainFileOrder> Files { get; set; }
        public List<DomainResponse> Responses { get; set; }
    }
}