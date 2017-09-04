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

        public bool HasIngredient(int cartId, int dishId, int ingredientId)
        {
            foreach (var item in _context.CartItemIngredients.Where(x => x.CartId == cartId && x.DishId == dishId).Select(i => i.Ingredient))
            {
                if (item.IngredientId == ingredientId)
                {
                    return true;
                }
            }
            return false;
        }

        public void RemoveIngredients(int dishId, int cartId)
        {
            var cartItemIngs = _context.CartItemIngredients.Where(x => x.DishId == dishId && x.CartId == cartId);
            foreach (var ing in cartItemIngs)
            {
                _context.Remove(ing);
            }
            _context.SaveChanges();
        }
    }
}
