using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HireGnome.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="You must enter a valid Email address")]
        [DataType(DataType.EmailAddress)]
        [Display(Name ="Introduce your Email address")]
        public string Email { get; set; }

        [Required(ErrorMessage ="The password that you introduced is not valid")]
        [DataType(DataType.Password)] // Masked field
        [Display(Name ="Introduce your password")]
        public string Password { get; set; }
    }
}