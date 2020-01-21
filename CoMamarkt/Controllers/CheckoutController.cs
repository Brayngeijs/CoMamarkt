using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoMamarkt.Data;
using CoMamarkt.Models;
using CoMaMarkt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoMamarkt.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<User> _userManager;


        public CheckoutController(ApplicationDbContext context, UserManager<User> userMan)
        {
            _context = context;
            _userManager = userMan;
        }

        [HttpGet]
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

            cvm.WagenItems = cartvm;
            cvm.TotalPrice = totalPrice;

            return View(cvm);
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(Checkout model)
        {
            Bestelling bestelling = new Bestelling();
            User user = await _userManager.GetUserAsync(HttpContext.User);
            bestelling.User = user;
            bestelling.BestellingProducts = new List<BestellingProduct>();

            List<WagenItem> cart = new List<WagenItem>();

            string cartString = HttpContext.Session.GetString("cart");
            if (cartString != null)
                cart = JsonConvert.DeserializeObject<List<WagenItem>>(cartString);

            foreach (WagenItem ci in cart)
            {
                Product p = _context.Product.Find(ci.ProductId);

                BestellingProduct ol = new BestellingProduct();
                ol.Amount = ci.Amount;
                ol.Price = p.Prijs;
                ol.Product = p;

                bestelling.BestellingProducts.Add(ol);

            }

            try
            {
                _context.Add(bestelling);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

            return View("confirm");
        }
    }
}