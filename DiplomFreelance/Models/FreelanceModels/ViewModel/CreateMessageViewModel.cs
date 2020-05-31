using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.ViewModel
{
    public class CreateMessageViewModel
    {
        public int ID_Chat { get; set; }
        public string Text { get; set; }
        public string Sender { get; set; }
        public DateTime Time_send { get; set; }
    }
}