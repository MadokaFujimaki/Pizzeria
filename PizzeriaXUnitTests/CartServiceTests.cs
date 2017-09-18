using Pizzeria.Data;
using Pizzeria.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Pizzeria.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Pizzeria.Controllers;

namespace PizzeriaXUnitTests
{
    public class CartServiceTests : BasePizzeriaTests
    {
        public override void InitializeDatabase()
        {
            base.InitializeDatabase();
            var context = serviceProvider.GetService<ApplicationDbContext>();
            context.CartItems.Add(new CartItem { CartItemId = 5, CartId = 3, Quantity = 1, DishId = 10 });
            context.CartItems.Add(new CartItem { CartItemId = 6, CartId = 3, Quantity = 2, DishId = 11 });
            context.CartItemIngredients.Add(new CartItemIngredient { CartItemId = 5, IngredientId = 100 });
            context.CartItemIngredients.Add(new CartItemIngredient { CartItemId = 5, IngredientId = 101 });
            context.CartItemIngredients.Add(new CartItemIngredient { CartItemId = 6, IngredientId = 101 });
            context.Ingredients.Add(new Ingredient { IngredientId = 100, Name = "AAA", Price = 2});
            context.Ingredients.Add(new Ingredient { IngredientId = 101, Name = "BBB", Price = 5});
            context.Dishes.Add(new Dish { DishId = 10, Price= 100});
            context.Dishes.Add(new Dish { DishId = 11, Price = 150 });
            context.Carts.Add(new Cart { CartId =3});

            context.SaveChanges();
        }

        [Fact]
        public void Return_Additional_Ingredients_Price()
        {
            var  context = serviceProvider.GetService<ApplicationDbContext>();
            var cartService = serviceProvider.GetService<CartService>();//Arrange
            var result = cartService.AddIngPrice(context.Ingredients.ToList(), 2);//Act
            Assert.Equal(result, 14);//Assert
        }

        [Fact]
        public void Get_All_CartItemIngredients_For_Cart()
        {
            //Arrange
            var context = serviceProvider.GetService<ApplicationDbContext>();
            var cartService = serviceProvider.GetService<CartService>();
            var ing1 = context.Ingredients.Where(x => x.IngredientId == 100).FirstOrDefault();
            var ing2 = context.Ingredients.Where(x => x.IngredientId == 101).FirstOrDefault();
            var ingList = new List<Ingredient> { ing1, ing2, ing2, ing2 };
            //Act
            var result = cartService.GetCartItemIng(3);
            //Assert
            Assert.Equal(result, ingList);
        }

        [Fact]
        public void Return_Total_Additional_Ingredients_Price()
        {
            //Arrange
            var cartService = serviceProvider.GetService<CartService>();
            //Act
            var ingList = cartService.GetCartItemIng(3);
            var result = cartService.AddIngTotalPrice(ingList);
            //Assert
            Assert.Equal(result, 17);
        }

        [Fact]
        public void Return_Total_Price()
        {
            //Arrange
            var httpContext = new DefaultHttpContext();
            httpContext.Session = new TestSession();
            httpContext.Session.SetInt32("CartId", 3);

            var context = serviceProvider.GetService<ApplicationDbContext>();
            var cartService = serviceProvider.GetService<CartService>();
            cartService = new CartService(context, serviceProvider, httpContext.Session);
        
            //Act
            var result = cartService.CalculateTotal(30);
            //Assert
            Assert.Equal(result, 430);
        }
    }
}
