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

            // Every cart must be attached to a user, but a user (ie Admin) could not have any cart
            mb.Entity<Users>().
                HasMany<Carts>(x => x.Carts);
                //WithRequired(x => x.User).
                //HasForeignKey(x => x.UserId);

            // A cart has un usuario opcional (o una identidad anónima) ligados
            mb.Entity<Carts>().
                HasOptional(x => x.User);

            mb.Entity<Carts>().
                HasOptional(x => x.User);
            /*
            mb.Entity<Users>().
                HasMany<Bills>(x => x.Bills).
                WithOptional(x => x.User). // One user may not have any bill
                HasForeignKey(x => x.UserId);

            // One to one
            mb.Entity<Carts>().
                HasOptional(x => x.Bill).   // A cart has one optional bill attached...
                WithRequired(x => x.Cart);  // And a Bill cannot be stored without a Cart attached


    */



        }
    }
}