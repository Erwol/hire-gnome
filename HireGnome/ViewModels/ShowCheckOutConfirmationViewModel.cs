using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using HireGnome.Models;

namespace HireGnome.ViewModels
{
    public class ShowCheckOutConfirmationViewModel
    {
        [Key]
        public int Id { get; set; }

        public Users User { get; set; }

        public Carts Cart { get; set; }
    }
}