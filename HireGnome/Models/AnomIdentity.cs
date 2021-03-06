﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HireGnome.Models
{
    public class AnomIdentity
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }

        public int CartId { get; set; }
        public virtual Carts Cart {get; set;}

        public AnomIdentity()
        {
            CreationDate = DateTime.Now;
            ModificationDate = DateTime.Now;
        }
    }
}