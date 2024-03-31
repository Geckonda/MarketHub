using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Domain.ViewModels
{
    public class LoginViewModel
    {
        [EmailAddress(ErrorMessage = "Введено неверно")]
        [MaxLength(50, ErrorMessage = "Email должен иметь длину не больше 20 символов")]
        [MinLength(5, ErrorMessage = "Email должен иметь длину не меньше 5 символов")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; } = string.Empty;
    }
}
