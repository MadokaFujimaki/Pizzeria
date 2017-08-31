//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Pizzeria.Data;
//using Pizzeria.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Pizzeria.Services
//{
//    public class CartService
//    {
//        private readonly ApplicationDbContext _context;

//        public List<CartItem> Item { get; set; }
//        public int CartId { get; set; }

//        public CartService(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public int GetTempCartId(ISession session)
//        {
//            if (!session.GetInt32("CartId").HasValue)
//            {
//                var tempCart = new CartService(_context) { Item = new List<CartItem>() };
//                _context.Carts.Add(tempCart);
//                _context.SaveChanges();
//                session.SetInt32("CartId", tempCart.CartId);
//            }
//            var cartId = session.GetInt32("CartId").Value;
//            return cartId;
//        }

//        //public async Task<Cart> GetCartForCurrentSession(ISession session)
//        //{
//        //    var cartItem = new CartItem();
//        //    cartItem.CartId = GetTempCartId(session);
//        //    cartItem.Dish = _context.Dishes.Find(cartItem.DishId);
//        //    _context.Add(cartItem);
//        //    await _context.SaveChangesAsync();
//        //    return cartItem;

//        //}

//        public async Task AddItemForCurrentSession(ISession session, int dishId)
//        {
//            var cartItem = new CartItem();
//            cartItem.CartId = GetTempCartId(session);
//            cartItem.Dish = _context.Dishes.Find(dishId);
//            cartItem.Quantity = 1;
//            _context.Add(cartItem);
//            await _context.SaveChangesAsync();
//        }

//        //public async Task DeleteItemForCurrentSession(ISession session, int)
//        //{

//        //}
//    }
//}
