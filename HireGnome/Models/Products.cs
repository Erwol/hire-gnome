using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HireGnome.Models
{
    public class Products
    {
        [Key]
        int id { get; set; }

        [Required]
        public string Details { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime AddedDate { get; set; }

        [Required]
        public float Price { get; set; }

        [Range(0, 1)]
        public float Offer { get; set; }
    }
}