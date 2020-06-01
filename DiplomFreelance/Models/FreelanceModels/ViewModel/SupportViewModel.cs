using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models.FreelanceModels.ViewModel
{
    public class SupportViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Электронная почта")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[+]?[(]?[0-9]{3}[)]?[-\s.]?[0-9]{3}[-\s.]?[0-9]{4,6}$", ErrorMessage = "Неверный формат")]
        [StringLength(15, ErrorMessage = "Значение {0} должно содержать не менее 11 символов.", MinimumLength = 11)]
        [DataType(DataType.Text)]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Тема сообщения")]
        public string Theme { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Сообщение")]
        public string Message { get; set; }
    }
}