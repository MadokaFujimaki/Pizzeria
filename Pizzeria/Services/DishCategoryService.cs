using Pizzeria.Data;
using Pizzeria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzeria.Services
{
    public class DishCategoryService
    {
        private readonly ApplicationDbContext _context;

        public DishCategoryService(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<DishCategory> GetCategory()
        {
            IEnumerable<DishCategory> categories = _context.DishCategories.Select(x => x);
            return categories;
        }


    }
}
