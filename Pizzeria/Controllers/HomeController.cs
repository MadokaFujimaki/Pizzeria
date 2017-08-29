using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.Models;
using Microsoft.AspNetCore.Http;
using Pizzeria.Services;

namespace Pizzeria.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
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

        //public CartService _cartService { get; set; }

        public IActionResult AlacarteAction(IFormCollection Form)
        {
            var key = Form.Keys.FirstOrDefault(k => k.Contains("-"));
            var dashPos = key.IndexOf("-");
            var action = key.Substring(0, dashPos);
            var id = int.Parse(key.Substring(dashPos + 1));
            switch (action)
            {
                //case "add":
                //    _cartService.AddItemForCurrentSession(HttpContext.Session, id);
                //    break;
                //case "remove":
                //    _cartService.DeleteItemForCurrentSession(HttpContext.Session,id);
                //    break;
                //case "customize":
                //    return RedirectToAction("Customize", "CartItems", new { Cart });
                //    break;
            }

            return RedirectToAction("");
        }

    }
}
