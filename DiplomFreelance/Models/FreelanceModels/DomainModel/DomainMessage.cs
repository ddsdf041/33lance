using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.DomainModel
{
    public class DomainMessage
    {
        public int ID { get; set; }
        public string Sender { get; set; }
        public string Text { get; set; }
        public DateTime Time_send { get; set; }
        public int ID_Chat { get; set; }
    }
}