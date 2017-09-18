using Microsoft.AspNetCore.Http;
using Pizzeria.Data;
using Pizzeria.Models;
using Pizzeria.Models.ManageViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzeria.Services
{
    public class OrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _services;
        private readonly ISession _session;

        public OrderService(ApplicationDbContext context, IServiceProvider services, ISession session)
        {
            _context = context;
            _services = services;
            _session = session;
        }

        public void SaveOrder(int cartId, PaymentViewModel user)
        {
            var order = new Order();
            order.OrderDateTime = DateTime.Now;
            order.CartId = cartId;
            order.Cart = _context.Carts.Where(x => x.CartId == cartId).First();
            order.CustomerName = user.CustomerName;
            order.Street = user.Street;
            order.PostalCode = user.PostalCode;
            order.City = user.City;
            _context.Add(order);

            _context.SaveChanges();
        }

        public Dish GetDish(int dishId)
        {
            return _context.Dishes.Where(x => x.DishId == dishId).FirstOrDefault();
        }

        public List<Ingredient> GetAddIngs(int cartItemId)
        {
            return _context.CartItemIngredients.Where(x => x.CartItemId == cartItemId).Select(x=>x.Ingredient).ToList();
        }

    }
}
