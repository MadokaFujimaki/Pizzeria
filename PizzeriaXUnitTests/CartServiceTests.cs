using Pizzeria.Data;
using Pizzeria.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Pizzeria.Models;
using System.Linq;

namespace PizzeriaXUnitTests
{
    public class CartServiceTests : BasePizzeriaTests
    {
        public override void InitializeDatabase()
        {
            base.InitializeDatabase();
            var context = serviceProvider.GetService<ApplicationDbContext>();
            context.Ingredients.Add(new Pizzeria.Models.Ingredient { IngredientId=100, Name = "AAA", Price = 2 });
            context.Ingredients.Add(new Pizzeria.Models.Ingredient { IngredientId=101, Name = "BBB", Price = 5 });
            context.SaveChanges();
        }

        [Fact]
        public void Return_Total_Additional_Ingredients_price()
        {
            var  context = serviceProvider.GetService<ApplicationDbContext>();
            var cartService = serviceProvider.GetService<CartService>();//Arrange
            var result = cartService.AddIngPrice(context.Ingredients.ToList(), 2);//Act
            Assert.Equal(result, 14);//Assert
        }

    }
}
