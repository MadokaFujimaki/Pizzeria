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
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The price must be greater than 1")]
        public int Price { get; set; }
        public List<DishIngredient> DishIngredients { get; set; }
        //[ScaffoldColumn(false)]
        public byte[] Image { get; set; }
        public int DishCategoryId { get; set; }
        [Display(Name = "Category")]
        public DishCategory DishCategory { get; set; }
        public List<CartItem> CartItems { get; set; }


    }
}
