using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.ViewModel
{
    public class CreateServiceViewModel
    {
        public int ID { get; set; }
        //[Required]
        [DataType(DataType.Text)]
        public string ID_Executor { get; set; }

        [Required(ErrorMessage = "Заполните поле заголовок")]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не более 100 символов.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите время выполнения услуги")]
        [DataType(DataType.Text)]
        public string Time_work { get; set; }

        [Required(ErrorMessage = "Заполните опыт работы в этой сфере")]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не более 100 символов.")]
        public string Expirience { get; set; }

        [Required(ErrorMessage = "Укажите цену за выполнение услуги")]
        [DataType(DataType.Text)]
        public decimal Price { get; set; }

        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не более 1000 символов.")]
        public string Notation { get; set; }

        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не более 200 символов.")]
        [DataType(DataType.Text)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Выберите меру, по которой оплачивается услуга")]
        [DataType(DataType.Text)]
        public int ID_Measure { get; set; }

        [Required(ErrorMessage = "Выберите место оказания услуги")]
        [DataType(DataType.Text)]
        public int ID_Place { get; set; }

        [Required(ErrorMessage = "Выберите подкатегорию услуги")]
        [DataType(DataType.Text)]
        public int ID_Subcategory { get; set; }

        public List<CategoryViewModel> categoryViewModels { get; set; }
    }
}