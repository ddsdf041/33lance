using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels
{
    public class Executor
    {
        public virtual string ID_User { get; set; }
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual byte[] Photo { get; set; }
        public virtual string Specialty { get; set; }
        public virtual string Telephone { get; set; }
        public virtual decimal Raiting { get; set; }
        public virtual string Description { get; set; }

        public virtual List<Service> Services { get; set; }
    }
}