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
        [Display(Name = "Name of this Gnome")]
        public string Name { get; set; }

        [Display(Name = "Some interesting details")]
        public string Details { get; set; }

        [Display(Name = "Make the Gnome public")]
        public bool IsPublic { get; set; }

        // TODO: Add Image model and link here
        [Display(Name = "Link to the Gnome Image")]
        public string Image { get; set; }

        /*
         * Trigonometric functions are available only for double
            Precision of double is far beyond anything you'll ever require for Lat/Lon values
            Third-Party modules (e.g. DotSpatial) also use double for coordinates
        */
        [Display(Name = "Latitude")]
        public double Latitude { get; set; }
        [Display(Name = "Longitude")]
        public double Longitude { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ModificationDate { get; set; }

        [Required]
        public double Price { get; set; }

        [Range(0, 100)]
        public int Offer { get; set; }

        // One product can be in different shopping carts
        public virtual ICollection<Carts> Carts { get; set; }

        
        public Products()
        {
            Offer = 0;
            IsPublic = true;
            CreationDate = DateTime.Now;
            ModificationDate = DateTime.Now;
            Image = "https://photos.smugmug.com/Travel/Poland/Wroclaw-Poland/i-Vrd5Vp8/0/L/Wroclaw-Poland-Europe-2919-L.jpg";
        }
    }
}
 