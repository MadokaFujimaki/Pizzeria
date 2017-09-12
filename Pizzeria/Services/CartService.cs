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
                _context.Dishes.Find(cartId).CartItems = _context.CartItems.Where(x => x.CartId == cartId).ToList();
                var cart = _context.Carts.Where(x => x.CartId == cartId).SingleOrDefault();
                //cart.CartItems.Select(x => x.CartItemIngredients).ToList()
                return cart;
            }
        }

        public List<string> GetCartItemIngName(int cartItemId)
        {
            var names = _context.CartItemIngredients.Where(x => x.CartItemId == cartItemId).Select(x => x.Ingredient.Name).ToList();
            return names;
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
                if (!exist)
                {
                    Cart cart = new Cart();
                    _context.Add(cart);
                    _context.SaveChanges();
                    _session.SetInt32("CartId", cart.CartId);

                    CartItem cartItem = new CartItem();
                    //cartItem.CartItemId = 0;
                    cartItem.CartId = cart.CartId;
                    cartItem.Dish = _context.Dishes.Find(dishId);
                    cartItem.Quantity = 1;

                    _context.Add(cartItem);
                    _context.SaveChanges();
                }
                else
                {
                    int cartId = _session.GetInt32("CartId").Value;
                    if (_context.Carts.Where(x => x.CartId == cartId).Select(x => x.CartItems) != null)
                    {
                        CartItem cartItem = new CartItem();
                        //cartItem.CartItemId = _context.CartItems.OrderByDescending(x => x.CartItemId).Select(x => x.CartItemId).FirstOrDefault() + 1;
                        cartItem.CartId = cartId;
                        //cartItem.DishId = dishId;
                        cartItem.Dish = _context.Dishes.Find(dishId);
                        cartItem.Quantity = 1;
                        _context.Add(cartItem);
                    }
                    else
                    {
                        CartItem cartItem = new CartItem();
                        ////cartItem.CartItemId = 0;
                        cartItem.CartId = cartId;
                        cartItem.Dish = _context.Dishes.Find(dishId);
                        cartItem.Quantity = 1;
                        _context.Add(cartItem);
                    }
                    _context.SaveChanges();
                    _context.Dishes.Find(cartId).CartItems = _context.CartItems.Where(x => x.CartId == cartId).ToList();
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

        public int AddIngTotalPrice(List<Ingredient> cartItemIngredients)
        {
            //int total = 0;
            if (cartItemIngredients != null)
            {
                //foreach (var cartItemIng in cartItemIngredients)
                //{
                //    total = cartItemIngredients.Select(x => x.Price)* 
                //}
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
            //return _context.CartItemIngredients.Where(x => x.CartItem.CartId == cartId).Select(x => x.Ingredient).ToList();'
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

        public void SaveOrder(int cartId, PaymentViewModel user)
        {
            var order = new Order
            {
                OrderDateTime = DateTime.Now,
                Total = _context.Carts.Where(x => x.CartId == cartId).Select(x => x.Total).FirstOrDefault(),
                CartItems = _context.CartItems.Where(x => x.CartId == cartId).ToList(),
                ApplicationUserId = _context.Carts.Where(x => x.CartId == cartId).Select(x => x.ApplicationUserId).FirstOrDefault(),
                ApplicationUser = new ApplicationUser
                {
                    CustomerName = user.CustomerName,
                    Street = user.Street,
                    PostalCode = user.PostalCode,
                    City = user.City,
                    CardId = user.CardId,
                    CreditCardNumber = user.CreditCardNumber,
                    NameOnCard = user.CreditCardNumber,
                    YYMM = user.YYMM,
                    CCV = user.CCV
                }
            };
            _context.Add(order);
            _context.SaveChanges();
    }

    public void RemoveCart(int cartId)
    {
        var cart = _context.Carts.Where(x => x.CartId == cartId).FirstOrDefault();
        _context.Remove(cart);
        _context.SaveChanges();
        _session.Remove("CartId");

            //var order = _context.Orders.Select(x => x);
    }
}
}
