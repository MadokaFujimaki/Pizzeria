﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Pizzeria.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string CustomerName { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }

        public int CardId { get; set; }
        public Card Card { get; set; }
        public int CreditCardNumber { get; set; }
        public string NameOnCard { get; set; }
        public int YYMM { get; set; }
        public int CCV { get; set; }
        
    }
}
