using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.ViewModel
{
    public class FileOrderViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

        public Guid ID_Order { get; set; }
    }
}