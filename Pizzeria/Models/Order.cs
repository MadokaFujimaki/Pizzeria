using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzeria.Models
{
    public class Order:Cart
    {
        public int OrderId { get; set; }
        public DateTime OrderDateTime { get; set; }
        //public List<OrderDish> OrderDishes { get; set; }
        //public int ApplicationUserId { get; set; }
        //public ApplicationUser ApplicationUser { get; set; }
        //public int TotalAmount { get; set; }

    }
}
