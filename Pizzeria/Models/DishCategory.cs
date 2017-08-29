using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzeria.Models
{
    public class DishCategory
    {
        public int DishCategoryId { get; set; }
        public string Discription { get; set; }
        public List<Dish> Dishes { get; set; }
    }
}
