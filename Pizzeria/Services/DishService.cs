using Pizzeria.Data;
using Pizzeria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzeria.Services
{
    public class DishService
    {
        private readonly ApplicationDbContext _context;

        public DishService(ApplicationDbContext context)
        {
            this._context = context;
        }


        public string IngredientList(int dishId)
        {
            var dishIngredientList = _context.Dishes.Where(di => di.DishId == dishId).Select(i => i.DishIngredients).ToList();
            var ingredients = new List<string>();
            foreach (var dishIngredients in dishIngredientList)
            {
                foreach (var ingredient in dishIngredients)
                {
                    var ingredientName = _context.Ingredients.Where(i => i.IngredientId == ingredient.IngredientId).Select(n => n.Name).Single();
                    ingredients.Add(ingredientName);
                    //ingredients += ingredientName + " ";
                }
            }
            return string.Join(",  ",ingredients.OrderBy(x => x).ToList());
        }

    }
}
