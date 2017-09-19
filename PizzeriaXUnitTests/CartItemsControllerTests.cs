using Microsoft.Extensions.DependencyInjection;
using Pizzeria.Controllers;
using Xunit;
using Pizzeria.Services;
using Pizzeria.Data;
using Pizzeria.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace PizzeriaXUnitTests
{
    public class CartItemsControllerTests : BasePizzeriaTests
    {
        public override void InitializeDatabase()
        {
            base.InitializeDatabase();
            var context = serviceProvider.GetService<ApplicationDbContext>();
            context.CartItems.Add(new CartItem { CartItemId = 5, CartId = 3, Quantity = 1, DishId = 10 });
            context.CartItemIngredients.Add(new CartItemIngredient { CartItemId = 5, IngredientId = 100 });
            context.CartItemIngredients.Add(new CartItemIngredient { CartItemId = 5, IngredientId = 101 });
            context.Ingredients.Add(new Ingredient { IngredientId = 100, Name = "AAA", Price = 2 });
            context.Ingredients.Add(new Ingredient { IngredientId = 101, Name = "BBB", Price = 5 });
            context.Dishes.Add(new Dish { DishId = 10, Price = 100 });
            context.SaveChanges();
        }

        [Fact]
        public void Return_CartItem_To_CartItemCustomizeView()
        {
            //Arrange
            var context = serviceProvider.GetService<ApplicationDbContext>();
            var controller = serviceProvider.GetService<CartItemsController>();

            //Act
            var cartItem = context.CartItems.Where( x=> x.CartItemId == 5).FirstOrDefault();
            var result = controller.Customize(5);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(cartItem, viewResult.Model);
        }
    }
}
