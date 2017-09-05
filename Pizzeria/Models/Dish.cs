using Microsoft.AspNetCore.Mvc;
using Pizzeria.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzeria.Models
{
    public class Dish
    {
        public int DishId { get; set; }
        public string Name { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "The price must be greater than 1")]
        public int Price { get; set; }
        public List<DishIngredient> DishIngredients { get; set; }
        public List<OrderDish> OrderDishes { get; set; }
        [ScaffoldColumn(false)]
        public byte[] Image { get; set; }
        public int DishCategoryId { get; set; }
        public DishCategory DishCategory { get; set; }
        public List<CartItem> CartItems { get; set; }


    }
}
