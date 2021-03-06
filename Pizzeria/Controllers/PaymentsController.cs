﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Text.Encodings.Web;
using Pizzeria.Services;
using Pizzeria.Models;
using Pizzeria.Models.ManageViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pizzeria.Data;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pizzeria.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly UrlEncoder _urlEncoder;
        private readonly CartService _cartService;
        private readonly ApplicationDbContext _context;

        //private const string AuthenicatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

        public PaymentsController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<ManageController> logger,
            UrlEncoder urlEncoder,
            CartService cartService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _urlEncoder = urlEncoder;
            _cartService = cartService;
            _context = context;
            _emailSender = emailSender;
        }

        public string StatusMessage { get; set; }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["CardId"] = new SelectList(_context.Cards, "CardId", "Name");

            var user = await _userManager.GetUserAsync(User);
            var model = new PaymentViewModel();
            if (user == null)
            {
                return View(model);
            }
            else
            {
                model = new PaymentViewModel
                {

                    PhoneNumber = user.PhoneNumber,
                    //StatusMessage = StatusMessage,
                    CustomerName = user.CustomerName,
                    Email = user.Email,
                    Street = user.Street,
                    PostalCode = user.PostalCode,
                    City = user.City
                };
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(PaymentViewModel model)
        {
            ViewData["CardId"] = new SelectList(_context.Cards, "CardId", "Name");
            if (!ModelState.IsValid)
            {
                return View(model);
            }
  
            return RedirectToAction("ComfirmPayment", model);
            //StatusMessage = "Your profile has been updated";
            //return RedirectToAction(nameof(Index));
        }

        public IActionResult ComfirmPayment(PaymentViewModel User)
        {
            User.Card = _context.Cards.Where(x => x.CardId == User.CardId).FirstOrDefault();
            return View(User);
        }

        [HttpPost]
        public IActionResult Receipt(int cartId,int total, [Bind("CustomerName,PhoneNumber,Email,Street,PostalCode,City,CreditCardNumber,NameOnCard,YYMM,CCV,CardId")] PaymentViewModel user)
        {
            _context.Carts.FirstOrDefault(x => x.CartId == cartId).Total = total;
           _logger.LogCritical($"To: {user.Email}, Subject: Confirmation of payment, Message: Thank you for your order!");

            _emailSender.SendEmailAsync(user.Email, "Confirmation of payment(Email)", "Thank you for your order!");

            return View(user);
        }
    }
}
