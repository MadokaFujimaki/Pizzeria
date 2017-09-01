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

            var adminUser = new ApplicationUser();
            adminUser.UserName = "admin@test.com";
            adminUser.Email = "admin@test.com";
            var adminUserResult = userManager.CreateAsync(adminUser, "Pa$$w0rd").Result;

            var adminRole = new IdentityRole { Name = "Admin"};
            var roleResult = roleManager.CreateAsync(adminRole).Result;

            userManager.AddToRoleAsync(adminUser, "Admin");

            //Om det inte finns Dishes i databasen
            if (context.Dishes.ToList().Count == 0)
            {
                var pizzaImage = LoadImage.GetPictureData("wwwroot/images/pizza.jpg");
                var carzonesImage = LoadImage.GetPictureData("wwwroot/images/carzone.jpg");
                var dessertImage = LoadImage.GetPictureData("wwwroot/images/dessert.jpg");

                var pizza = new DishCategory { Discription = "Pizza" };
                var carzone = new DishCategory { Discription = "Carzone" };
                var dessert = new DishCategory { Discription = "Dessert" };

                context.DishCategories.Add(pizza);
                context.DishCategories.Add(carzone);
                context.DishCategories.Add(dessert);

                var cheese = new Ingredient { Name = "Cheese", Price=30 };
                var tomato = new Ingredient { Name = "Tomato", Price = 10 };
                var ham = new Ingredient { Name = "Ham", Price = 25 };
                var pineapple = new Ingredient { Name = "Pineapple", Price = 10 };
                var bacon = new Ingredient { Name = "Bacon", Price = 25 };
                var onions = new Ingredient { Name = "Onions", Price = 10 };
                var mushrooms = new Ingredient { Name = "Mushrooms", Price = 20 };
                var apple = new Ingredient { Name = "Apple", Price = 20 };
                var currySauce = new Ingredient { Name = "Curry Sauce", Price = 15 };
                var banana = new Ingredient { Name = "Banana", Price = 10 };

                var capricciosa = new Dish { Name = "Capricciosa", Price = 79 , Image=pizzaImage, DishCategory = pizza };
                var margaritha = new Dish { Name = "Margaritha", Price = 69, Image = pizzaImage, DishCategory = pizza };
                var hawaii = new Dish { Name = "Hawaii", Price = 85, Image = pizzaImage, DishCategory = pizza };
                var tropical = new Dish { Name = "Tropical", Price = 75, Image = pizzaImage, DishCategory = pizza };
                var veggie = new Dish { Name = "Veggie", Price = 95, Image = pizzaImage, DishCategory = pizza };
                var calzone = new Dish { Name = "Calzone", Price = 100, Image = carzonesImage, DishCategory = carzone };
                var calzoneSp = new Dish { Name = "Calzone SP", Price = 115, Image = carzonesImage, DishCategory = carzone };
                var applePie = new Dish { Name = "Apple Pie", Price = 70, Image = dessertImage, DishCategory = dessert };

                var capricciosaHam = new DishIngredient { Dish = capricciosa, Ingredient = ham };
                var capricciosaMushrooms = new DishIngredient { Dish = capricciosa, Ingredient = mushrooms };
                capricciosa.DishIngredients = new List<DishIngredient>();
                capricciosa.DishIngredients.Add(capricciosaHam);
                capricciosa.DishIngredients.Add(capricciosaMushrooms);

                var margarithaCheese = new DishIngredient { Dish = margaritha, Ingredient = cheese };
                margaritha.DishIngredients = new List<DishIngredient>();
                margaritha.DishIngredients.Add(margarithaCheese);

                var hawaiiHam = new DishIngredient { Dish = hawaii, Ingredient = ham };
                var hawaiiPineapple = new DishIngredient { Dish = hawaii, Ingredient = pineapple };
                hawaii.DishIngredients = new List<DishIngredient>();
                hawaii.DishIngredients.Add(hawaiiHam);
                hawaii.DishIngredients.Add(hawaiiPineapple);

                var tropicalBanana = new DishIngredient { Dish = tropical, Ingredient = banana };
                var tropicalCurrySauce = new DishIngredient { Dish = tropical, Ingredient = currySauce };
                tropical.DishIngredients = new List<DishIngredient>();
                tropical.DishIngredients.Add(tropicalBanana);
                tropical.DishIngredients.Add(tropicalCurrySauce);

                var veggieMushrooms = new DishIngredient { Dish = veggie, Ingredient = mushrooms };
                var veggieOnions = new DishIngredient { Dish = veggie, Ingredient = onions };
                var veggieTomato = new DishIngredient { Dish = veggie, Ingredient = tomato };
                veggie.DishIngredients = new List<DishIngredient>();
                veggie.DishIngredients.Add(veggieMushrooms);
                veggie.DishIngredients.Add(veggieOnions);
                veggie.DishIngredients.Add(veggieTomato);

                var calzoneHam = new DishIngredient { Dish = calzone, Ingredient = ham };
                calzone.DishIngredients = new List<DishIngredient>();
                calzone.DishIngredients.Add(calzoneHam);

                var calzoneSpHam = new DishIngredient { Dish = calzoneSp, Ingredient = ham };
                var calzoneSpMushrooms = new DishIngredient { Dish = calzoneSp, Ingredient = mushrooms };
                calzoneSp.DishIngredients = new List<DishIngredient>();
                calzoneSp.DishIngredients.Add(calzoneSpHam);
                calzoneSp.DishIngredients.Add(calzoneSpMushrooms);

                var applePieApple = new DishIngredient { Dish = applePie, Ingredient = apple };
                applePie.DishIngredients = new List<DishIngredient>();
                applePie.DishIngredients.Add(applePieApple);

                context.Dishes.Add(capricciosa);
                context.Dishes.Add(margaritha);
                context.Dishes.Add(hawaii);
                context.Dishes.Add(tropical);
                context.Dishes.Add(veggie);
                context.Dishes.Add(calzone);
                context.Dishes.Add(calzoneSp);
                context.Dishes.Add(applePie);


                //context.AddRange(capricciosa, margaritha, hawaii);
                context.SaveChanges();
            }
        }
    }
}
