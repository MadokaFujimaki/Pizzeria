using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pizzeria.Controllers;
using Pizzeria.Data;
using Pizzeria.Services;
using System;
using Xunit;

namespace PizzeriaXUnitTests
{
    public class BasePizzeriaTests
    {
        public readonly IServiceProvider serviceProvider;

        public BasePizzeriaTests()
        {
            var efServiceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(b =>
                b.UseInMemoryDatabase("Pizzadatabase")
                .UseInternalServiceProvider(efServiceProvider));

            services.AddTransient<CartService>();
            //services.AddTransient<CategoryService>();
            services.AddTransient<DishService>();
            services.AddTransient<IngredientService>();
            services.AddTransient<HomeController>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddTransient<ISession, TestSession>();

            serviceProvider = services.BuildServiceProvider();
            services.AddSingleton<IServiceProvider>(serviceProvider);

            InitializeDatabase();
        }

        public virtual void InitializeDatabase()
        {
        }
    }
}
