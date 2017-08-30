using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Data;
using Pizzeria.Models;

namespace Pizzeria.Controllers
{
    public class DishCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DishCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DishCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.DishCategories.ToListAsync());
        }

        // GET: DishCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishCategory = await _context.DishCategories
                .SingleOrDefaultAsync(m => m.DishCategoryId == id);
            if (dishCategory == null)
            {
                return NotFound();
            }

            return View(dishCategory);
        }

        // GET: DishCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DishCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DishCategoryId,Discription")] DishCategory dishCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dishCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dishCategory);
        }

        // GET: DishCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishCategory = await _context.DishCategories.SingleOrDefaultAsync(m => m.DishCategoryId == id);
            if (dishCategory == null)
            {
                return NotFound();
            }
            return View(dishCategory);
        }

        // POST: DishCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DishCategoryId,Discription")] DishCategory dishCategory)
        {
            if (id != dishCategory.DishCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dishCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DishCategoryExists(dishCategory.DishCategoryId))
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
            return View(dishCategory);
        }

        // GET: DishCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishCategory = await _context.DishCategories
                .SingleOrDefaultAsync(m => m.DishCategoryId == id);
            if (dishCategory == null)
            {
                return NotFound();
            }

            return View(dishCategory);
        }

        // POST: DishCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dishCategory = await _context.DishCategories.SingleOrDefaultAsync(m => m.DishCategoryId == id);
            _context.DishCategories.Remove(dishCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DishCategoryExists(int id)
        {
            return _context.DishCategories.Any(e => e.DishCategoryId == id);
        }
    }
}
