using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.ViewModel
{
    public class ChatViewModel
    {
        public int ID { get; set; }
        public CustomerViewModel Customer { get; set; }
        public ExecutorViewModel Executor { get; set; }
    }
}