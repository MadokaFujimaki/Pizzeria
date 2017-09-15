using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pizzeria.Controllers;
using Pizzeria.Data;
using Pizzeria.Models;
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

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //var httpContext = new DefaultHttpContext();
            //httpContext.Session = new TestSession();

            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<CartService>();
            //services.AddTransient<CategoryService>();
            services.AddTransient<DishService>();
            services.AddTransient<IngredientService>();
            services.AddTransient<HomeController>();
            services.AddTransient<PaymentsController>();
            //services.AddSingleton<IHttpContextAccessor>(new DefaultHttpContext().);
            services.AddTransient<ISession, TestSession>();

            serviceProvider = services.BuildServiceProvider();
            services.AddSingleton<IServiceProvider>(serviceProvider);

            InitializeDatabase();
        }

        public virtual void InitializeDatabase()
        {
        }
    }
}
