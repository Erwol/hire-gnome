using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using HireGnome.Models;

namespace HireGnome.ViewModels
{
    public class ShowUserCartsViewModel
    {
        public Users User { get; set; }
        public List<Carts> Carts { get; set; }
    }
}