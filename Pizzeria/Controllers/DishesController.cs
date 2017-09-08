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
    public class DishesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly DishService _dishService;

        public DishesController(ApplicationDbContext context, DishService dishService)
        {
            _context = context;
            _dishService = dishService;
        }

        // GET: Dishes
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Dishes.Include(d => d.DishIngredients).ThenInclude(d => d.Ingredient).Include(c => c.DishCategory).ToListAsync());

            var applicationDbContext = _context.Dishes.Include(d => d.DishCategory);
            return View(await _context.Dishes.Include(di => di.DishCategory).ToListAsync());
        }

        // GET: Dishes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes
                 .Include(d => d.DishIngredients)
                .ThenInclude(di => di.Ingredient)
                .SingleOrDefaultAsync(m => m.DishId == id);
            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        // GET: Dishes/Create
        public IActionResult Create()
        {
            ViewData["DishCategoryId"] = new SelectList(_context.DishCategories, "DishCategoryId", "Discription");
            return View();
        }

        // POST: Dishes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DishId,Name,Price,DishCategoryId")] Dish dish, IFormCollection collection)
        {
            List<Ingredient> testList = new List<Ingredient>();
            foreach (var item in collection.Keys.Where(m => m.StartsWith("ingredient-")))
            {
                var listIngredient = _context.Ingredients.FirstOrDefault(d => d.IngredientId == Int32.Parse(item.Remove(0, 11)));
                testList.Add(listIngredient);
                DishIngredient di = new DishIngredient() { Dish = dish, Ingredient = listIngredient };
                _context.DishIngredients.Add(di);
            }

            if (ModelState.IsValid)
            {
                _context.Add(dish);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DishCategoryId"] = new SelectList(_context.DishCategories, "DishCategoryId", "Discription", dish.DishCategoryId);
            return View(dish);
        }

        // GET: Dishes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes.SingleOrDefaultAsync(m => m.DishId == id);
            if (dish == null)
            {
                return NotFound();
            }
            ViewData["DishCategoryId"] = new SelectList(_context.DishCategories, "DishCategoryId", "Discription", dish.DishCategoryId);
            return View(dish);
        }

        // POST: Dishes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DishId,Name,Price,DishCategoryId")] Dish dish, IFormCollection collection)
        {
            _dishService.RemoveIngredients(id);
            List<Ingredient> testList = new List<Ingredient>();
            foreach (var item in collection.Keys.Where(m => m.StartsWith("ingredient-")))
            {
                var listIngredient = _context.Ingredients.FirstOrDefault(d => d.IngredientId == Int32.Parse(item.Remove(0, 11)));
                testList.Add(listIngredient);
                DishIngredient di = new DishIngredient() { Dish = dish, Ingredient = listIngredient };
                _context.DishIngredients.Add(di);
            }

            //foreach (var item in collection.Keys.Where(m => m.StartsWith("ingredient-")))
            //{
            //    var listIngredient = _context.Ingredients.FirstOrDefault(d => d.IngredientId == Int32.Parse(item.Remove(0, 11)));
            //    foreach (var ing in _context.Ingredients)
            //    {
            //        if (listIngredient.IngredientId == ing.IngredientId)
            //        {
            //            DishIngredient di = new DishIngredient() { Dish = dish, Ingredient = listIngredient };
            //            _context.DishIngredients.Add(di);
            //        }
            //    }
            //}

            if (id != dish.DishId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dish);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DishExists(dish.DishId))
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
            ViewData["DishCategoryId"] = new SelectList(_context.DishCategories, "DishCategoryId", "Discription", dish.DishCategoryId);
            return View(dish);
        }

        // GET: Dishes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes
                .SingleOrDefaultAsync(m => m.DishId == id);
            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        // POST: Dishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dish = await _context.Dishes.SingleOrDefaultAsync(m => m.DishId == id);
            _context.Dishes.Remove(dish);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DishExists(int id)
        {
            return _context.Dishes.Any(e => e.DishId == id);
        }


        public FileContentResult GetImg(int id)
        {
            byte[] byteArray = _context.Dishes.Find(id).Image;
            return byteArray != null
                ? new FileContentResult(byteArray, "image/jpeg")
                : null;
        }
    }
}
