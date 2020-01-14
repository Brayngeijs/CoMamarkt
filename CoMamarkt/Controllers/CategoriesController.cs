using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoMaMarkt.Models;
using CoMamarkt.Data;
using System.Xml;
using System.Globalization;
using CoMamarkt.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace CoMamarkt.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public readonly IWebHostEnvironment webHostEnvironment;

        public CategoriesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var subcategorieën = await _context.Categorie.Include(c => c.Subcategorieen).ToListAsync();
            return View(subcategorieën);
        }

        public IActionResult Subcategorieën(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var categorie = _context.Categorie.Where(m => m.Id == id).Include(p => p.Subcategorieen).ThenInclude(s => s.Products).FirstOrDefault();
            if (categorie == null)
            {
                return NotFound();
            }
            return View(categorie);
        }

        public async Task<IActionResult> LoadXml()
        {
            XmlDocument xdoc = new XmlDocument();

            xdoc.Load(
                "https://supermaco.starwave.nl/api/categories"
                );

            XmlNodeList elemList = xdoc.GetElementsByTagName("Category");

            for (int i = 0; i < elemList.Count; i++)
            {
                Categorie c = new Categorie();
                c.Naam = elemList[i].SelectSingleNode("./Name").InnerXml;
                _context.Update(c);
            }


            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
