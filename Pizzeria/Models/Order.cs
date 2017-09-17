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
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        //public int TotalAmount { get; set; }
        //public List<CartItem> CartItems { get; set; }
        //public List<OrderDish> OrderDishes { get; set; }
        public string CustomerName { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }

    }
}
