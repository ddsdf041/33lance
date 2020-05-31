
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels
{
    public class Subcategory
    {

        public virtual int ID { get; set; }
        public virtual int ID_Category { get; set; } //да
        public virtual string Name { get; set; }


    }
}