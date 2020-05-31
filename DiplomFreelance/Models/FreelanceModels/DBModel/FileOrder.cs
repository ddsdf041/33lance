using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels
{
    public class FileOrder
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public Guid ID_Order { get; set; }
    }
}