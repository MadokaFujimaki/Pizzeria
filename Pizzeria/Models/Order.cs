using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzeria.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public List<OrderDish> OrderDishes { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
        public DateTime OrderDateTime { get; set; }
        public int TotalAmount { get; set; }

    }
}
