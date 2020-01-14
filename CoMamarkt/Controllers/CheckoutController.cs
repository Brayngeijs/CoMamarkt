using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoMamarkt.Data;
using CoMamarkt.Models;
using CoMaMarkt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoMamarkt.Controllers
{
    public class CheckoutController : Controller
    {
        private ApplicationDbContext _context;

        public CheckoutController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Checkout()
        {
            Checkout cvm = new Checkout();

            List<WagenItem> cart = new List<WagenItem>();

            string cartString = HttpContext.Session.GetString("cart");
            if (cartString != null)
                cart = JsonConvert.DeserializeObject<List<WagenItem>>(cartString);


            List<Winkelwagen> cartvm = new List<Winkelwagen>();

            double totalPrice = 0;

            foreach (WagenItem ci in cart)
            {
                Winkelwagen civm = new Winkelwagen();

                civm.ProductId = ci.ProductId;
                civm.Amount = ci.Amount;

                Product p = _context.Product.Find(ci.ProductId);

                civm.Naam = p.Naam;
                civm.Prijs = p.Prijs;
                civm.Image = p.Image;

                totalPrice += ci.Amount * p.Prijs;

                cartvm.Add(civm);
            }

            cvm.CartItems = cartvm;
            cvm.TotalPrice = totalPrice;

            return View(cvm);
        }
    }
}