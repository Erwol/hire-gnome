using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireGnome.Models
{
    public class Carts
    {
        [Key]
        public int Id { get; set; }

        // Cart < 1 - * > Products
        public virtual ICollection<Products> Products { get; set; }

        // Cart < 1 - 1 > User
        // [Required] Let's try to create a shopping cart for anonymous users (Doubt)
        public int UserId { get; set; }
        public virtual Users User { get; set; }

        public string Name { get; set; }        // A way to personalize the cart
        public string Public { get; set; }      // Cart listed in "Inspiration" tab
        public string Billed { get; set; }      // A cart that has been shipped
        public string MainCart { get; set; }    // Tells the app that this cart should be the more accesible

        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }

        // [Required]
        [DataType(DataType.DateTime)]
        public DateTime ModificationDate { get; set; }

        /*
        [ForeignKey("Bill")]
        public int BillId { get; set; }

        public virtual Bills Bill { get; set; }
        */

    }
}