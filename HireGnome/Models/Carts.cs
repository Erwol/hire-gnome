using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HireGnome.Models
{
    public class Carts
    {
        [Key]
        public int Id { get; set; }

        public virtual ICollection<Products> Products { get; set; }

        // A cart can belong to a registered user or to an anom visitor that has an AnomIdentity
        //  nullable FK property
        // public int? UserId { get; set; }
        public virtual Users User { get; set; }

        //public int? AnomIdentityId { get; set; }
        public virtual AnomIdentity AnomIdentity { get; set; }

        public string Name { get; set; }

        public bool IsPublic { get; set; }      // A cart may be made "public", so other users can see what this specific user uses to buy
        public bool IsBilled { get; set; }      // A cart that has been shipped
        public bool IsMainCart { get; set; }    // Tells the app that this cart should be the more accesible

        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ModificationDate { get; set; }

        /*
        [ForeignKey("Bill")]
        public int BillId { get; set; }

        public virtual Bills Bill { get; set; }
        */


        // Constructor
        public Carts()
        {
            IsBilled = false;
            IsMainCart = false;
            IsPublic = false;
            CreationDate = DateTime.Now;
            ModificationDate = DateTime.Now;
        }

    }
}