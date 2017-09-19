using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzeria.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }

        [Required]
        [Display(Name = "Customer name")]
        public string CustomerName { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        [Display(Name = "Postal code")]
        [MaxLength(5, ErrorMessage = "The field Postal code must be numeric with a length of '5'.")]
        [MinLength(5, ErrorMessage = "The field Postal code must be numeric with a length of '5'.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "The field Postal code must be numeric")]
        public string PostalCode { get; set; }

        [Required]
        public string City { get; set; }
    }
}
