using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TryAuthorization.ViewModels
{
    public class RegisterModel
    {
       
        
            [Required(ErrorMessage = "Не указан email")]
            public string Email { get; set; }
            [Required(ErrorMessage = "Не указан пароль")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            [DataType(DataType.Password)]
            [Compare("Password",ErrorMessage ="Введен неверный пароль")]
            public string ConfirmPassword { get; set; }
        

    }
}
