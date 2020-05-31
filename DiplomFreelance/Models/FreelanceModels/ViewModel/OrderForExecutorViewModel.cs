using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.ViewModel
{
    public class OrderForExecutorViewModel
    {

        public List<CategoryViewModel> Categories { get; set; }
        public IPagedList<OrderViewModel> Orders { get; set; }
    }
}