using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Data;
using Pizzeria.Models;
using Microsoft.AspNetCore.Http;
using Pizzeria.Services;

namespace Pizzeria.Controllers
{
    public class CartItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IngredientService _ingredientService;

        public CartItemsController(ApplicationDbContext context, IngredientService IngredientService)
        {
            _context = context;
            _ingredientService = IngredientService;
        }

        // GET: CartItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CartItems.Include(c => c.Cart).Include(c => c.Dish);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CartItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItems
                .Include(c => c.Cart)
                .Include(c => c.Dish)
                .SingleOrDefaultAsync(m => m.CartId == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // GET: CartItems/Create
        public IActionResult Create()
        {
            ViewData["CartId"] = new SelectList(_context.Carts, "CartId", "CartId");
            ViewData["DishId"] = new SelectList(_context.Dishes, "DishId", "DishId");
            return View();
        }

        // POST: CartItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartItemId,CartId,DishId,Quantity")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cartItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartId"] = new SelectList(_context.Carts, "CartId", "CartId", cartItem.CartId);
            ViewData["DishId"] = new SelectList(_context.Dishes, "DishId", "DishId", cartItem.DishId);
            return View(cartItem);
        }

        // GET: CartItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItems.SingleOrDefaultAsync(m => m.CartId == id);
            if (cartItem == null)
            {
                return NotFound();
            }
            ViewData["CartId"] = new SelectList(_context.Carts, "CartId", "CartId", cartItem.CartId);
            ViewData["DishId"] = new SelectList(_context.Dishes, "DishId", "DishId", cartItem.DishId);
            return View(cartItem);
        }

        // POST: CartItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CartItemId,CartId,DishId,Quantity")] CartItem cartItem)
        {
            if (id != cartItem.CartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartItemExists(cartItem.CartId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartId"] = new SelectList(_context.Carts, "CartId", "CartId", cartItem.CartId);
            ViewData["DishId"] = new SelectList(_context.Dishes, "DishId", "DishId", cartItem.DishId);
            return View(cartItem);
        }

        // GET: CartItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItems
                .Include(c => c.Cart)
                .Include(c => c.Dish)
                .SingleOrDefaultAsync(m => m.CartId == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // POST: CartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cartItem = await _context.CartItems.SingleOrDefaultAsync(m => m.CartId == id);
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartItemExists(int id)
        {
            return _context.CartItems.Any(e => e.CartId == id);
        }

        public IActionResult Customize(int cartItemId)
        {
            var cartItem =  _context.CartItems.Where(x => x.CartItemId == cartItemId).FirstOrDefault();
            cartItem.Dish = _context.Dishes.FirstOrDefault(x => x.DishId == cartItem.DishId);
            cartItem.Dish.DishIngredients = _context.DishIngredients.Where(x => x.DishId == cartItem.DishId).ToList();
            //_context.Update(cartItem);
            //_context.SaveChanges();
            foreach (var item in cartItem.Dish.DishIngredients)
            {
                item.Ingredient = _context.Ingredients.Where(x => x.IngredientId == item.IngredientId).FirstOrDefault();
            }
            return View(cartItem);
        }

        [HttpPost]
        public IActionResult Customize([Bind("CartItemId,Quantity,CartId,DishId")] CartItem cartItem, IFormCollection collection)
        {
            _ingredientService.RemoveIngredients(cartItem.DishId, cartItem.CartId);
            foreach (var item in collection.Keys.Where(m => m.StartsWith("ingredient-")))
            {
                var listIngredient = _context.Ingredients.FirstOrDefault(d => d.IngredientId == Int32.Parse(item.Remove(0, 11)));
                CartItemIngredient di = new CartItemIngredient() { DishId=cartItem.DishId, CartId = cartItem.CartId, Ingredient = listIngredient };
                _context.CartItemIngredients.Add(di);
                _context.SaveChanges();
            }
            var quantity = cartItem.Quantity;
            cartItem = _context.CartItems.Where(x => x.CartItemId == cartItem.CartItemId).FirstOrDefault();
            cartItem.Dish = _context.Dishes.FirstOrDefault(x => x.DishId == cartItem.DishId);
            cartItem.Dish.DishIngredients = _context.DishIngredients.Where(x => x.DishId == cartItem.DishId).ToList();
            cartItem.CartItemIngredients = _context.CartItemIngredients.Where(x => x.CartItem.CartItemId == cartItem.CartItemId).ToList();
            cartItem.Quantity = quantity;
            foreach (var item in cartItem.Dish.DishIngredients)
            {
                item.Ingredient = _context.Ingredients.Where(x => x.IngredientId == item.IngredientId).FirstOrDefault();
            }
            _context.Update(cartItem);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
            //return View(cartItem);
        }
    }
}
