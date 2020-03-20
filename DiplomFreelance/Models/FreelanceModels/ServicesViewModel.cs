using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels
{
    public class ServicesViewModel
    {
        public virtual List<Executor> Executors { get; set; }
        public virtual List<Service> Services { get; set; }
    }
}