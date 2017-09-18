using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pizzeria.Data;
using Pizzeria.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PizzeriaXUnitTests
{
    public class IngredientServiceTests : BasePizzeriaTests
    {
        public override void InitializeDatabase()
        {
            base.InitializeDatabase();
            var context = serviceProvider.GetService<ApplicationDbContext>();
            context.Ingredients.Add(new Pizzeria.Models.Ingredient { Name = "AAA", Price = 2 });
            context.Ingredients.Add(new Pizzeria.Models.Ingredient { Name = "BBB", Price = 5 });
            context.SaveChanges();
        }

        [Fact]
        public void All_Are_Sorted()
        {
            var _ingredients = serviceProvider.GetService<IngredientService>();//Arrange

            var ings = _ingredients.AllIngredients();//Act

            Assert.Equal(2, ings.Count);//Assert
            Assert.Equal(ings[0].Name, "AAA");
            Assert.Equal(ings[1].Name, "BBB");
        }
    }
}

