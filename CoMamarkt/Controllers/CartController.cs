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
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddToCart(int id)
        {
            List<WagenItem> cart = new List<WagenItem>();
            string cartString = HttpContext.Session.GetString("cart");
            if (cartString != null)
            {
                cart = JsonConvert.DeserializeObject<List<WagenItem>>(cartString);
            }


            WagenItem item = new WagenItem
            {
                Amount = 1,
                ProductId = id

            };
            WagenItem item2 = cart.Find(c => c.ProductId == id);
            if (item2 != null)
            {
                item2.Amount++;
            }
            else
            {
                cart.Add(item);
            }

            cartString = JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString("cart", cartString);

            return RedirectToAction("index");
        }

        public IActionResult Cart()
        {
            List<WagenItem> cart = new List<WagenItem>();
            string cartString = HttpContext.Session.GetString("cart");
            if (cartString != null)
            {
                cart = JsonConvert.DeserializeObject<List<WagenItem>>(cartString);
            }

            List<Winkelwagen> cartvm = new List<Winkelwagen>();
            foreach (WagenItem ci in cart)
            {
                Winkelwagen civm = new Winkelwagen();
                civm.ProductId = ci.ProductId;
                civm.Amount = ci.Amount;

                Product p = _context.Product.Find(ci.ProductId);

                civm.Naam = p.Naam;
                civm.Prijs = p.Prijs;
                civm.Image = p.Image;

                cartvm.Add(civm);

            }

            return View(cartvm);
        }
        public IActionResult DeleteItem(int? id)
        {
            List<WagenItem> cart = new List<WagenItem>();

            string cartString = HttpContext.Session.GetString("cart");
            if (cartString != null)
                cart = JsonConvert.DeserializeObject<List<WagenItem>>(cartString);


            WagenItem item = cart.Find(c => c.ProductId == id);
            cart.Remove(item);

            cartString = JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString("cart", cartString);

            return RedirectToAction("Cart");
        }
    }
}