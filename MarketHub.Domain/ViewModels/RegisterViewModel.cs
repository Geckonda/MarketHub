using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Domain.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Укажите Имя")]
        [MaxLength(20, ErrorMessage = "Имя должен иметь длину не больше 20 символов")]
        [MinLength(3, ErrorMessage = "Имя должен иметь длину не меньше 3 символов")]
        [Display(Name = "Имя")]
        public string Name { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Введено неверно")]
        [Display(Name = "Электронная почта")]
        public string Email { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Укажите пароль")]
        [MinLength(6, ErrorMessage = "Пароль должен иметь длину не меньше 6 символов")]
        [Display(Name = "Пароль")]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [Display(Name = "Подтверждение пароля")]
        public string PasswordConfirm { get; set; } = string.Empty;
    }
}
