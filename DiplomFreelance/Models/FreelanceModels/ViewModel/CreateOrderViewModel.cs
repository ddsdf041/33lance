using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.ViewModel
{
    public class CreateOrderViewModel 
    {
        [Required]
        [DataType(DataType.Text)]
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Заполните поле заголовок")]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не более 100 символов.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Выберите подкатегорию заказа")]
        [DataType(DataType.Text)]
        public int ID_Subcategory { get; set; }

        [Required(ErrorMessage = "Добавьте описание")]
        [DataType(DataType.Text)]
        [StringLength(1000, ErrorMessage = "Значение {0} должно содержать не более 1000 символов.")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Deadline { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public int ID_Measure { get; set; }

        [DataType(DataType.Text)]
        public string ID_Executor { get; set; }

        [Required(ErrorMessage = "Укажите бюджет услуги")]
        [DataType(DataType.Text)]
        public decimal Budget { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public int ID_Place { get; set; }

        [DataType(DataType.Text)]
        public string Address { get; set; }

        
        
        public HttpPostedFileBase[] Photo { get; set; }

        public List<CategoryViewModel> categoryViewModels { get; set; }
        public ExecutorViewModel executorViewModel { get; set; }
    }
}