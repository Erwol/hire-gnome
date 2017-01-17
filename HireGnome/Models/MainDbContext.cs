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

        public DbSet<Lists> Lists { get; set; } // We've to add this here in order to make db queries

        public DbSet<Users> Users { get; set; } // This tells the app that is the representation of the table with its exact name

        protected override void OnModelCreating(DbModelBuilder mb)
        {
            // mb.Entity<User>().HasKey(u => u.UserId);
            //mb.Entity<Task>().HasKey(t => t.TaskId);
            mb.Entity<Carts>().HasMany(x => x.Products).WithMany();
            //modelBuilder.Entity<Person>().HasMany(x => x.Roles).WithMany();

        }
    }
}