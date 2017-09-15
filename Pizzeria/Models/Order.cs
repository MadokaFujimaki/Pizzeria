using Pizzeria.Models.ManageViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzeria.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDateTime { get; set; }
        public int Total { get; set; }
        public List<CartItem> CartItems { get; set; }
        //public List<OrderDish> OrderDishes { get; set; }
        //public int UserId { get; set; }
        public PaymentViewModel User { get; set; }

    }
}
