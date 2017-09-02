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
            _session = _services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
        }

        public Cart GetCart()
        {
            byte[] cartIdBytes = new byte[4];
            bool exist = _session.TryGetValue("CartId", out cartIdBytes);
            if (!exist)
            {
                //Cart cart = new Cart();
                //_context.Add(cart);
                //_context.SaveChanges();
                //_session.SetInt32("CartId", cart.CartId);
                //return cart;
                return null;
            }
            else
            {
                var cartId = _session.GetInt32("CartId").Value;
                _context.Dishes.Find(cartId).Item = _context.Item.Where(x => x.CartId == cartId).ToList();
                var cart = _context.Carts.Where(x => x.CartId == 1).SingleOrDefault();
                return cart;
            }
        }

        //public IEnumerable<CartItem> GetCartItem()
        //{
        //    byte[] cartIdBytes = new byte[4];
        //    bool exist = _session.TryGetValue("CartId", out cartIdBytes);
        //    if (!exist)
        //    {
        //        Cart cart = new Cart();
        //        _context.Add(cart);
        //        _context.SaveChanges();
        //        _session.SetInt32("CartId", cart.CartId);
        //        return null;
        //    }
        //    else
        //    {
        //        var cartId = _session.GetInt32("CartId").Value;
        //        var cartItem = _context.Item.Where(x => x.CartId == cartId);
        //        return cartItem;
        //    }
        //}


        public void AddDish(int dishId)
        {
            byte[] cartIdBytes = new byte[4];
            bool exist = _session.TryGetValue("CartId", out cartIdBytes);
            {
                if (! exist)
                {
                    Cart cart = new Cart();
                    _context.Add(cart);
                    _context.SaveChanges();
                    _session.SetInt32("CartId", cart.CartId);

                    CartItem cartItem = new CartItem();
                    cartItem.CartId = cart.CartId;
                    cartItem.Dish = _context.Dishes.Find(dishId);
                    cartItem.Quantity = 1;

                    _context.Add(cartItem);
                    _context.SaveChanges();
                }
                else
                {
                    int cartId = _session.GetInt32("CartId").Value;
                    var ci = _context.Item.FirstOrDefault(x => x.CartId == cartId && x.DishId == dishId);
                    if (ci != null)
                    {
                        ci.Quantity += 1;
                    }
                    else
                    {
                        CartItem cartItem = new CartItem();
                        cartItem.CartId = cartId;
                        //cartItem.DishId = dishId;
                        cartItem.Dish = _context.Dishes.Find(dishId);
                        cartItem.Quantity = 1;
                        _context.Add(cartItem);
                    }

                    _context.SaveChanges();
                    _context.Dishes.Find(cartId).Item = _context.Item.Where(x => x.CartId == cartId).ToList();
                    //var cart1 = _context.Carts.Find(cartId).Item;
                    //var cart = _context.Carts.Where(x => x.CartId == cartId).SingleOrDefault();
                }
            }
        }
    }
}
