using Pizzeria.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzeria.Services
{
    public class DishService
    {
        public ApplicationDbContext _context { get; set; }

        public DishService(ApplicationDbContext context)
        {
            this._context = context;
        }


    }
}
