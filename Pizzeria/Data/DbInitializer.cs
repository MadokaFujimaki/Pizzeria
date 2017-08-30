using Microsoft.AspNetCore.Identity;
using Pizzeria.Models;
using Pizzeria.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzeria.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var aUser = new ApplicationUser();
            aUser.UserName = "student@test.com";
            aUser.Email = "student@test.com";
            var r = userManager.CreateAsync(aUser, "Pa$$w0rd").Result;

            var adminRole = new IdentityRole { Name = "Admin"};
            var roleResult = roleManager.CreateAsync(adminRole).Result;

            var adminUser = new ApplicationUser();
            adminUser.UserName = "admin@test.com";
            adminUser.Email = "admin@test.com";
            var adminUserResult = userManager.CreateAsync(adminUser, "Pa$$w0rd").Result;

            userManager.AddToRoleAsync(adminUser, "Admin");

            //Om det inte finns Dishes i databasen
            if (context.Dishes.ToList().Count == 0)
            {
                var pizzaImage = LoadImage.GetPictureData("wwwroot/images/pizza.jpg");

                var classicPizza = new DishCategory { Discription = "Classic Pizza" };
                var italianPizza = new DishCategory { Discription = "Italian Pizza" };
                var amerikanPizza = new DishCategory { Discription = "Amerikan Pizza" };

                context.DishCategories.Add(classicPizza);
                context.DishCategories.Add(italianPizza);
                context.DishCategories.Add(amerikanPizza);

                var cheese = new Ingredient { Name = "Cheese" };
                var tomatoe = new Ingredient { Name = "Tomatoe" };
                var ham = new Ingredient { Name = "Ham" };

                var capricciosa = new Dish { Name = "Capricciosa", Price = 79 , Image=pizzaImage, DishCategory = classicPizza };
                var margaritha = new Dish { Name = "Margaritha", Price = 69, Image = pizzaImage, DishCategory = italianPizza };
                var hawaii = new Dish { Name = "Hawaii", Price = 85, Image = pizzaImage, DishCategory = amerikanPizza };

                var capricciosaCheese = new DishIngredient { Dish = capricciosa, Ingredient = cheese };
                var capricciosaTomatoe = new DishIngredient { Dish = capricciosa, Ingredient = tomatoe };
                var capricciosaHam = new DishIngredient { Dish = capricciosa, Ingredient = ham };

                capricciosa.DishIngredients = new List<DishIngredient>();
                capricciosa.DishIngredients.Add(capricciosaTomatoe);
                capricciosa.DishIngredients.Add(capricciosaCheese);
                capricciosa.DishIngredients.Add(capricciosaHam);
                context.Dishes.Add(capricciosa);
                context.Dishes.Add(margaritha);
                context.Dishes.Add(hawaii);





                //context.AddRange(capricciosa, margaritha, hawaii);
                context.SaveChanges();
            }
        }
    }
}
