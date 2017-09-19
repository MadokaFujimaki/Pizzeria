using Pizzeria.Models.ManageViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzeria.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        [Display(Name = "Order date")]
        public DateTime OrderDateTime { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        [Display(Name = "Customer name")]
        public string CustomerName { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
    }
}
