using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.ViewModel
{
    public class CreateResponseViewModel
    {
        [Required(ErrorMessage = "Укажите цену")]
        [DataType(DataType.Text)]
        public decimal Price { get; set; }

        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не более 1000 символов.")]
        public string Notation { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public Guid ID_Order { get; set; }

        [DataType(DataType.Text)]
        public int ID_Executor { get; set; }

        public DateTime Date { get; set; }
    }
}