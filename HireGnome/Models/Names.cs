using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HireGnome.Models
{
    public class Names
    {
        [Key]
        public int Id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public int phone { get; set; }
    }
}