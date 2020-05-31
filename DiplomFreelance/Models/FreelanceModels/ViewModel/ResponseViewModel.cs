using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.ViewModel
{
    public class ResponseViewModel
    {
        public int ID { get; set; }

        public decimal Price { get; set; }
        public string Notation { get; set; }
        public Guid ID_Order { get; set; }
        public ExecutorViewModel Executor { get; set; }
        public DateTime Date { get; set; }
    }
}