using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.ViewModel
{
    public class OrderDetailViewModel
    {
        public OrderViewModel Order { get; set; }
        public CreateResponseViewModel Response { get; set; }

        public ResponseViewModel ResponseVM { get; set; }
    }
}