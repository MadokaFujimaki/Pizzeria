using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzeria.Models.ManageViewModels
{
    public class PaymentViewModel
    {
        public string StatusMessage { get; set; }

        [Required]
        [Display(Name = "Customer name")]
        public string CustomerName { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        [Display(Name = "Postal code")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "The field Postal code must be numeric")]
        public string PostalCode { get; set; }

        [Required]
        public string City { get; set; }

        [Display(Name = "Card")]
        public int CardId { get; set; }
        public Card Card { get; set; }

        [Required]
        [Display(Name = "Credit card number")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "The field Credit card number must be numeric")]
        public string CreditCardNumber { get; set; }

        [Required]
        [Display(Name = "Name on card")]
        public string NameOnCard { get; set; }

        [Required]
        [MaxLength(4, ErrorMessage = "The field CCV must be numeric with a length of '4'.")]
        [MinLength(4, ErrorMessage = "The field CCV must be numeric with a length of '4'.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "The field YYMM must be numeric")]
        public string YYMM { get; set; }

        [Required]
        [MaxLength(3, ErrorMessage = "The field CCV must be numeric with a length of '3'.")]
        [MinLength(3, ErrorMessage = "The field CCV must be numeric with a length of '3'.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "The field CCV must be numeric")]
        public string CCV { get; set; }
    }
}
