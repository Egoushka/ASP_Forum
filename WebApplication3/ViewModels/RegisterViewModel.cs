using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace WebApplication3.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name ="Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 5)]
        [Display(Name ="Password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password",ErrorMessage = "Passwords don't match")]
        [DataType(DataType.Password)]
        [Display(Name ="Comfirm password")]
        public string PasswordConfirm { get; set; }
    }
}
