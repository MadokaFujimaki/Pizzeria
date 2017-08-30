﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Models;
using Pizzeria.Services;
using Microsoft.AspNetCore.Identity;

namespace Pizzeria.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // DishId och IngredientId blir primary key (Många till Många)




            builder.Entity<DishIngredient>()
                .HasKey(di => new { di.DishId, di.IngredientId });

            builder.Entity<DishIngredient>()
                .HasOne(di => di.Dish)
                .WithMany(d => d.DishIngredients)
                .HasForeignKey(di => di.DishId);

            builder.Entity<DishIngredient>()
                .HasOne(di => di.Ingredient)
                .WithMany(i => i.DishIngredients)
                .HasForeignKey(di => di.IngredientId);


            builder.Entity<CartItemIngredient>()
               .HasKey(di => new { di.CartItemId, di.IngredientId });

            builder.Entity<CartItemIngredient>()
               .HasOne(di => di.CartItem)
               .WithMany(d => d.CartItemIngredients)
               .HasForeignKey(di => di.CartItemId);

            builder.Entity<CartItemIngredient>()
               .HasOne(di => di.Ingredient)
               .WithMany(i => i.CartItemIngredients)
               .HasForeignKey(di => di.IngredientId);


            builder.Entity<OrderDish>()
                .HasKey(od => new { od.OrderId, od.DishId });

            builder.Entity<OrderDish>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDishes)
                .HasForeignKey(od => od.OrderId);

            builder.Entity<OrderDish>()
                .HasOne(od => od.Dish)
                .WithMany(d => d.OrderDishes)
                .HasForeignKey(od => od.DishId);




            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            //builder.Ignore<IdentityUserLogin<string>>();
            //builder.Ignore<IdentityUserRole<string>>();
            //builder.Ignore<IdentityUserClaim<string>>();
            //builder.Ignore<IdentityUserToken<string>>();
            //builder.Ignore<IdentityUser<string>>();
            //builder.Ignore<ApplicationUser>();
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDish> OrderDishes { get; set; }
        public DbSet<DishCategory> DishCategories { get; set; }
        public DbSet<Dish> Dishes { get; set; } //Databas collection of dish
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<DishIngredient> DishIngredients { get; set; }
        public DbSet<CartItemIngredient> CartItemIngredient { get; set; }
        //public DbSet<CartService> Carts { get; set; }


        //public DbSet<Order> Orders { get; set; }
        //public DbSet<OrderDish> OrderDishes { get; set; }
    }
}
