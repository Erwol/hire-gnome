﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HireGnome.Models
{
    public class Users
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)] // Masked field
        public string Password { get; set; }

        [HiddenInput(DisplayValue = false)] // Temporary data storage
        public string ReturnUrl { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }
    }
}