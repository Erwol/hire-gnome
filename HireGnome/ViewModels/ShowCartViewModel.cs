using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using HireGnome.Models;

namespace HireGnome.ViewModels
{
    public class ShowCartViewModel
    {
        [Key]
        public int Id { get; set; }

        public virtual List<String> Products { get; set; }

        public int UserId { get; set; }
        public string Name { get; set; }
    }
}