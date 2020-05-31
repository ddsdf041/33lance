using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.ViewModel
{
    public class CustomerForAdminViewModel
    {
        public CustomerViewModel Customer { get; set; }
        public List<OrderViewModel> Orders { get; set; }
    }
}