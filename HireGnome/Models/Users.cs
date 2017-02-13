using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireGnome.Models
{
    public class Users
    {
        [Key]  
        public int Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }

        [Required(ErrorMessage ="Introduce your country")]
        public string Country { get; set; }

        [Required(ErrorMessage ="An address must be given")]
        public string Address { get; set; }

        // One to many relation with roles
        public int RolId { get; set; }
        [ForeignKey("RolId")]
        public virtual Roles Rol { get; set; }
        
        public virtual ICollection<Carts> Carts { get; set; }
        
        public virtual ICollection<Bills> Bills { get; set; }

        public bool IsActive { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ModificationDate { get; set; }

        public Users()
        {
            IsActive = false;

            CreationDate = DateTime.Now;
            ModificationDate = DateTime.Now;
        }
    }
}