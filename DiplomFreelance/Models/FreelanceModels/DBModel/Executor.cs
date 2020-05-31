using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels
{
    public class Executor
    {
        public string ID_User { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Photo { get; set; }
        public string Specialty { get; set; }
        public string Telephone { get; set; }
        public decimal Raiting { get; set; }
        public string Description { get; set; }
        public bool IsBanned { get; set; }

        //public virtual ICollection<Service> Services { get; set; }
    }
}