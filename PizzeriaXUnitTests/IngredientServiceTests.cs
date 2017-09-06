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
    //public class IngredientServiceTests
    //{
    //    private readonly IServiceProvider _serviceProvider;

    //    public IngredientServiceTests()
    //    {
    //        var efServiceProvider = new ServiceCollection()
    //            .AddEntityFrameworkInMemoryDatabase()
    //            .BuildServiceProvider();

    //        var services = new ServiceCollection();

    //        services.AddDbContext<ApplicationDbContext>(b =>
    //        b.UseInMemoryDatabase("Pizzadatabas")
    //        .UseInternalServiceProvider(efServiceProvider));

    //        services.AddTransient<IngredientService>();

    //        _serviceProvider = services.BuildServiceProvider();
    //    }


    //    [Fact]
    //    public void All_Are_Sorted()
    //    {
    //        var _ingredients = _serviceProvider.GetService<IngredientService>();
    //        var ings = _ingredients.AllIngredients();
    //        Assert.Equal(ings.Count,0);
    //    }
    //}

    public class IngredientServiceTests : BasePizzeriaTests
    {
        public override void InitializeDatabase()
        {
            base.InitializeDatabase();
            var context = serviceProvider.GetService<ApplicationDbContext>();
            context.Ingredients.Add(new Pizzeria.Models.Ingredient { Name = "BBB", Price = 2 });
            context.Ingredients.Add(new Pizzeria.Models.Ingredient { Name = "AAA", Price = 5 });
            context.SaveChanges();
        }

        [Fact]
        public void All_Are_Sorted()
        {
            var _ingredients = serviceProvider.GetService<IngredientService>();//Arrange
            var ings = _ingredients.AllIngredients();//Act
            //Assert.Equal(ings.Count, 0);
            Assert.Equal(2, ings.Count);//Assert
            Assert.Equal(ings[0].Name, "AAA");
            Assert.Equal(ings[1].Name, "BBB");
        }
    }
}

