using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireGnome.Models
{
    public class Bills
    {
        [Key]
        public int Id { get; set; }

        public virtual Users User { get; set; }

        public virtual Carts Cart { get; set; }

        public double FinalPrice { get; set; }



    }
}