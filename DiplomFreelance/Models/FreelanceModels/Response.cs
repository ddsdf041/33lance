using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels
{
    public class Response
    {

        public int ID_Executor { get; set; }
        public int ID_Order { get; set; }
        public decimal Price { get; set; }
        public string Notation { get; set; }
        public int ID_Measure { get; set; }

    }
}