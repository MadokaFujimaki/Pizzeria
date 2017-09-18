using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzeria.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; } 
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public int DishId { get; set; }
        public Dish Dish { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "The quantity must be greater than 1")]
        public int Quantity { get; set; } = 0;
        public List<CartItemIngredient> CartItemIngredients { get; set; }
    }
}
