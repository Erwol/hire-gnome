using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HireGnome.Models
{
    public class Carts
    {
        [Key]
        public int Id { get; set; }

        // Cart < 1 - * > Products
        public virtual ICollection<Products> Products { get; set; }

        // Cart < 1 - 1 > User
        public int UserId { get; set; }
        public virtual Users User { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ModificationDate { get; set; }
    }
}