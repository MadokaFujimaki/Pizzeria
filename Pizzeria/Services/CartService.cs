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
                return null;
            }
            else
            {
                var cartId = _session.GetInt32("CartId").Value;
                _context.Dishes.Find(cartId).Item = _context.CartItems.Where(x => x.CartId == cartId).ToList();
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
                    cartItem.CartItemId = 0;
                    cartItem.CartId = cart.CartId;
                    cartItem.Dish = _context.Dishes.Find(dishId);
                    cartItem.Quantity = 1;

                    _context.Add(cartItem);
                    _context.SaveChanges();
                }
                else
                {
                    int cartId = _session.GetInt32("CartId").Value;
                    var ci = _context.CartItems.FirstOrDefault(x => x.CartId == cartId && x.DishId == dishId);
                    if (ci != null)
                    {
                        ci.Quantity += 1;
                    }
                    else
                    {
                        if (_context.Carts.Where(x => x.CartId == cartId).Select(x => x.CartItems) != null)
                        {
                            CartItem cartItem = new CartItem();
                            cartItem.CartItemId = _context.CartItems.OrderByDescending(x => x.CartItemId).Select(x => x.CartItemId).FirstOrDefault() + 1;
                            cartItem.CartId = cartId;
                            //cartItem.DishId = dishId;
                            cartItem.Dish = _context.Dishes.Find(dishId);
                            cartItem.Quantity = 1;
                            _context.Add(cartItem);
                        }
                        else
                        {
                            CartItem cartItem = new CartItem();
                            cartItem.CartItemId = 0;
                            cartItem.CartId = cartId;
                            cartItem.Dish = _context.Dishes.Find(dishId);
                            cartItem.Quantity = 1;
                            _context.Add(cartItem);
                        }         
                    }
                    _context.SaveChanges();
                    _context.Dishes.Find(cartId).Item = _context.CartItems.Where(x => x.CartId == cartId).ToList();
                }
            }
        }

        public void DeleteDish(int cartItemId)
        {
            //int cartId = _session.GetInt32("CartId").Value;
            //var cart = _context.Carts.FirstOrDefault(x => x.CartId == cartId);
            var cartItem = _context.CartItems.Where(x => x.CartItemId == cartItemId).FirstOrDefault();
            _context.Remove(cartItem);
            _context.SaveChanges();

            //    var dishIngs = _context.DishIngredients.Where(x => x.DishId == dishId);

            //    foreach (var ing in dishIngs)
            //    {
            //        _context.Remove(ing);
            //    }
            //    _context.SaveChanges();
            //}
        }

        public int CalculateTotal()
        {
            var total = 0;
            byte[] cartIdBytes = new byte[4];
            bool exist = _session.TryGetValue("CartId", out cartIdBytes);
            if (exist)
            {
                int cartId = _session.GetInt32("CartId").Value;
                if (_context.Carts.FirstOrDefault(x => x.CartId == cartId).CartItems != null)
                {
                    foreach (var cartItem in _context.Carts.FirstOrDefault(x => x.CartId == cartId).CartItems)
                    {
                        if (cartItem.Quantity != 0)
                        {
                            total += cartItem.Dish.Price * cartItem.Quantity;
                        }
                        else
                        {
                            total += cartItem.Dish.Price;
                        }
                    }
                }
            }
            return total;
        }
    }
}
