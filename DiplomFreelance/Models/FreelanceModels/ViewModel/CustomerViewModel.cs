using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.ViewModel
{
    public class CustomerViewModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsBanned { get; set; }
    }
}