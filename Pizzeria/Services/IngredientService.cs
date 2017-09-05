using Pizzeria.Data;
using Pizzeria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzeria.Services
{
    public class IngredientService
    {
        private readonly ApplicationDbContext _context;

        public IngredientService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Ingredient> AllIngredients()
        {
            return _context.Ingredients.ToList();
        }

        public bool HasIngredient(int cartItemId, int ingredientId)
        {
            foreach (var item in _context.CartItemIngredients.Where(x => x.CartItemId == cartItemId).Select(i => i.Ingredient))
            {
                if (item.IngredientId == ingredientId)
                {
                    return true;
                }
            }
            return false;
        }

        public void RemoveIngredients(int cartItemId)
        {
            var cartItemIngs = _context.CartItemIngredients.Where(x => x.CartItemId == cartItemId);
            foreach (var ing in cartItemIngs)
            {
                _context.Remove(ing);
            }
            _context.SaveChanges();
        }
    }
}
