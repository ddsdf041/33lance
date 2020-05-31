using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.ViewModel
{
    public class MessageViewModel
    {
        public int ID { get; set; }
        public string Sender { get; set; }
        public string Text { get; set; }
        public DateTime Time_send { get; set; }

    }


}