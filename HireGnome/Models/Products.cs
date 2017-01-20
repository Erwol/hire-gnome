using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireGnome.Models
{
    public class Products
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; } // Without public

        [Required]
        public string Details { get; set; }

        public string Public { get; set; }

        [Required]
        public string Name { get; set; }
        
        public string AddedDate { get; set; }
        public string ModifiedDate { get; set; }

        //[DataType(DataType.Date)]
        //public DateTime ModifiedDate { get; set; }

        [Required]
        public double Price { get; set; }

        [Range(0, 100)]
        public double Offer { get; set; }

        public virtual ICollection<Carts> Carts { get; set; }
    }
}