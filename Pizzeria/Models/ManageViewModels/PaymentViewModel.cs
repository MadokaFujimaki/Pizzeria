using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzeria.Models.ManageViewModels
{
    public class PaymentViewModel
    {

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }
        [Display(Name = "Customer name")]
        public string CustomerName { get; set; }
        public string Street { get; set; }
        [Display(Name = "Postal code")]
        public string PostalCode { get; set; }
        public string City { get; set; }

        [Display(Name = "Card")]
        public int CardId { get; set; }
        public Card Card { get; set; }
        [Display(Name = "Credit card number")]
        public int CreditCardNumber { get; set; }
        [Display(Name = "Name on card")]
        public string NameOnCard { get; set; }
        public int YYMM { get; set; }
        public int CCV { get; set; }
    }
}
