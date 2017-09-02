using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.Models;
using Microsoft.AspNetCore.Http;
using Pizzeria.Services;
using Pizzeria.Data;
using Microsoft.EntityFrameworkCore;

namespace Pizzeria.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CartService _cartService;

        public HomeController(ApplicationDbContext context, CartService  cartService)
        {
            _context = context;
            _cartService = cartService;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult> Index()
        {
            return View(await _context.Dishes.Include(x => x.DishCategory).ToListAsync());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AlacarteAction(IFormCollection Form)
        {
            var key = Form.Keys.FirstOrDefault(k => k.Contains("-"));
            var dashPos = key.IndexOf("-");
            var action = key.Substring(0, dashPos);
            var id = int.Parse(key.Substring(dashPos + 1));
            switch (action)
            {
                case "add":
                    _cartService.AddDish(id);
                    break;
                //case "remove":
                //    _cartService.DeleteItemForCurrentSession(HttpContext.Session, id);
                //    break;
                //case "customize":
                //    return RedirectToAction("Customize", "CartItems", new { Cart });
                //    break;
            }

            return RedirectToAction("");
        }
    }
}
