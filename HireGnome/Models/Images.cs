using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace HireGnome.Models
{
    public class Images
    {
        [Key]
        public int Id { get; set; }

        public string url { get; set; }
    }
}