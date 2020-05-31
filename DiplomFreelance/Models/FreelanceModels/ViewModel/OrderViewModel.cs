using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.ViewModel
{
    public class OrderViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public decimal Budget { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public int ID_Status { get; set; }
        public string ID_Customer { get; set; }
        public ExecutorViewModel Executor { get; set; }
        public string Subcategory{ get; set; }
        public string Category { get; set; }
        public string Place { get; set; }
        public string Measure { get; set; }
        public DateTime Date { get; set; }
        public bool IsBanned { get; set; }

        public CustomerViewModel Customer { get; set; }
        public List<FileOrderViewModel> Files { get; set; }
        public List<ResponseViewModel> Responses { get; set; }
    }
}