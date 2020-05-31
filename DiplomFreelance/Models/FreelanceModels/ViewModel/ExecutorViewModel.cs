using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace DiplomFreelance.Models.FreelanceModels.ViewModel
{
    public class ExecutorViewModel
    {
        public string ID_User { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string City { get; set; }


        [Display(Name = "Ваше фото")]
        public string Photo { get; set; }

        public HttpPostedFileBase PhotoFile { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Specialty { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }
     
        public decimal Raiting { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        public bool IsBanned { get; set; }

        public ICollection<ServiceViewModel> Services { get; set; }
        //public ICollection<OrderViewModel> Orders { get; set; }
        //public ICollection<ResponseViewModel> Responses { get; set; }
    }
}