using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Pizzeria.Data;
using Pizzeria.Models;
using Pizzeria.Models.ManageViewModels;
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

        public CartService(ApplicationDbContext context, IServiceProvider services, ISession session)
        {
            _context = context;
            _services = services;
            _session = session;
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
                var cart = _context.Carts.Where(x => x.CartId == cartId).SingleOrDefault();
                return cart;
            }
        }

        public List<string> GetCartItemIngName(int cartItemId)
        {
            var names = _context.CartItemIngredients.Where(x => x.CartItemId == cartItemId).Select(x => x.Ingredient.Name).ToList();
            return names;
        }

        public void AddDish(int dishId)
        {
            byte[] cartIdBytes = new byte[4];
            bool exist = _session.TryGetValue("CartId", out cartIdBytes);
            {
                if (!exist)
                {
                    Cart cart = new Cart();
                    _context.Add(cart);
                    _context.SaveChanges();
                    _session.SetInt32("CartId", cart.CartId);

                    CartItem cartItem = new CartItem();
                    cartItem.CartId = cart.CartId;
                    cartItem.Dish = _context.Dishes.Find(dishId);
                    cartItem.DishId = dishId;
                    cartItem.Quantity = 1;

                    _context.Add(cartItem);
                    _context.SaveChanges();
                }
                else
                {
                    int cartId = _session.GetInt32("CartId").Value;

                    CartItem cartItem = new CartItem();
                    cartItem.CartId = cartId;
                    cartItem.Dish = _context.Dishes.Find(dishId);
                    cartItem.DishId = dishId;
                    cartItem.Quantity = 1;
                    _context.Add(cartItem);

                    _context.SaveChanges();
                }
            }
        }

        public void DeleteDish(int cartItemId)
        {
            var cartItem = _context.CartItems.Where(x => x.CartItemId == cartItemId).FirstOrDefault();
            _context.Remove(cartItem);
            _context.SaveChanges();
        }

        public int AddIngTotalPrice(List<Ingredient> cartItemIngredients)
        {
            if (cartItemIngredients != null)
            {
                return cartItemIngredients.Select(x => x.Price).Sum();
            }
            return 0;
        }

        public int CalculateTotal(int ingPrice)
        {
            var total = ingPrice;
            byte[] cartIdBytes = new byte[4];
            bool exist = _session.TryGetValue("CartId", out cartIdBytes);
            if (exist)
            {
                int cartId = _session.GetInt32("CartId").Value;
                if (_context.Carts.FirstOrDefault(x => x.CartId == cartId).CartItems != null)
                {
                    var cartItems = _context.CartItems.Where(x => x.CartId == cartId);
                    foreach (var cartItem in cartItems)
                    {
                        cartItem.Dish = _context.Dishes.Where(x => x.DishId == cartItem.DishId).FirstOrDefault();
                        if (cartItem.Quantity != 0)
                        {
                            total = total + cartItem.Dish.Price * cartItem.Quantity;
                        }
                        else
                        {
                            total = total + cartItem.Dish.Price;
                        }
                    }
                }
            }
            return total;
        }

        public List<Ingredient> GetCartItemIng(int cartId)
        {
            var cartItemIngs = new List<Ingredient>();
            var cartItems = _context.CartItems.Where(x => x.CartId == cartId);
            foreach (var cartItem in cartItems)
            {
                for (int i = 0; i < cartItem.Quantity; i++)
                {
                    foreach (var cartItemIngredient in cartItem.CartItemIngredients = _context.CartItemIngredients.Where(x => x.CartItem.CartItemId == cartItem.CartItemId).ToList())
                    {
                        cartItemIngs.Add(_context.Ingredients.Where(x => x.IngredientId == cartItemIngredient.IngredientId).FirstOrDefault());
                    }
                }
            }
            return cartItemIngs;
        }

        public int AddIngPrice(List<Ingredient> ingredients, int quantity)
        {
            return ingredients.Select(x => x.Price).Sum() * quantity;
        }

        public void RemoveCartSession(int cartId)
        {
            _session.Remove("CartId");
        }

    }
}
