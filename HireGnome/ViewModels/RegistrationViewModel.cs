using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireGnome.ViewModels
{
    public class RegistrationViewModel
    {
        [Display(Name="Email address")]
        [Required(ErrorMessage ="That's not a valid email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name ="Password")]
        [Required]
        [DataType(DataType.Password)] // Masked field
        public string Password { get; set; }

        [Display(Name = "User name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Second Name")]
        public string SecondName { get; set; }

        [Display(Name = "Where are you from?")]
        public string Country { get; set; }
    }
}