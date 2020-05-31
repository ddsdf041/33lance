using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels
{
    public class DomainCustomer
    {
        public string ID_User { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsBanned { get; set; }
    }
}