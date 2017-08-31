using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Pizzeria.Data;
using Pizzeria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzeria.Services
{
    public class CartService
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _services;
        private readonly ISession _session;

        public CartService(ApplicationDbContext context, IServiceProvider services)
        {
            _context = context;
            _services = services;
            _session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

        }

        public void AddDish(int dishId)
        {
            byte[] cartIdBytes = new byte[4];
            bool exist = _session.TryGetValue("cartId", out cartIdBytes);
            {
                if (! exist)
                {
                    Cart cart = new Cart();
                    _context.Add(cart);
                    _context.SaveChanges();
                    _session.SetInt32("cartId", cart.CartId);
                }
                else
                {
                    int cartId = _session.GetInt32("cartId").Value;
                    CartItem cartItem = new CartItem();
                    cartItem.CartId = cartId;
                    cartItem.DishId = dishId;

                    _context.Add(cartItem);
                    _context.SaveChanges();
                }
            }
        }

        public int Total()
        {
            return 123;
        }
    }
}
