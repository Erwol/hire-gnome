using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using HireGnome.Models;

namespace HireGnome
{
    public class MainDbContext : DbContext // Inherited in order to load the previously installed Entity Framework
    {
        public MainDbContext() : base("name=DefaultConnection") // Here's where we link the app with the previously created database. The name of the connection may be changed in the Web.config file.
        {

        }

        public DbSet<Products> Products { get; set; }

        public DbSet<Carts> Carts { get; set; }

        public DbSet<Users> Users { get; set; } // This tells the app that is the representation of the table with its exact name

        public DbSet<Roles> Roles { get; set; }

        public DbSet<Names> Names { get; set; }

        public DbSet<AnomIdentity> AnomIdentities { get; set; }

        // public DbSet<Images> Images { get; set; }

        protected override void OnModelCreating(DbModelBuilder mb)
        {
            
            // Many to many
            mb.Entity<Carts>().
                HasMany(x => x.Products).
                WithMany(x => x.Carts);

            // One to many
            mb.Entity<Roles>().
                HasMany<Users>(x => x.Users).
                WithRequired(x => x.Rol).
                HasForeignKey(x => x.RolId);

            // The carts are attached to Users... or Anom Identities
            mb.Entity<Carts>().
                HasOptional(x => x.User).
                WithMany(x => x.Carts);

            mb.Entity<AnomIdentity>()
                .HasRequired(x => x.Cart)
                .WithOptional(x => x.AnomIdentity);
            /*
            mb.Entity<Carts>()
                .HasOptional(x => x.AnomIdentity)
                .WithOptionalPrincipal(x => x.Cart);
                */
            /*
            mb.Entity<Users>().
                HasMany<Carts>(x => x.Carts);

            mb.Entity<AnomIdentity>().
                HasOptional<Carts>(x => x.Cart);

            mb.Entity<Carts>().
                HasOptional(x => x.User);

            mb.Entity<Carts>().
                HasOptional(x => x.AnomIdentity);
            */



        }
    }
}