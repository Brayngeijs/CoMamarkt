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

namespace CoMamarkt.Controllers
{
    [Area("CMS")]
    public class SubcategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubcategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Subcategories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Subcategorie.Include(s => s.Categorie);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Subcategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subcategorie = await _context.Subcategorie
                .Include(s => s.Categorie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subcategorie == null)
            {
                return NotFound();
            }

            return View(subcategorie);
        }

        // GET: Subcategories/Create
        public IActionResult Create()
        {
            ViewData["CategorieNaam"] = new SelectList(_context.Categorie, "Id", "Naam");
            return View();
        }

        // POST: Subcategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naam,CategorieId")] Subcategorie subcategorie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subcategorie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategorieId"] = new SelectList(_context.Categorie, "Id", "Id", subcategorie.CategorieId);
            return View(subcategorie);
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

                XmlNodeList subCategories = elemList[i].SelectNodes("./Subcategory");

                for (int y = 0; y < subCategories.Count; y++)
                {
                    Subcategorie sc = new Subcategorie();

                    var categorieNaam = elemList[i].SelectSingleNode("./Name").InnerXml;
                    var categorie = _context.Categorie.First(c => c.Naam == categorieNaam);
                    sc.Categorie = categorie;

                    sc.Naam = subCategories[y].SelectSingleNode("./Name").InnerXml;                    


                    _context.Add(sc);
                }
            }


            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // GET: Subcategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subcategorie = await _context.Subcategorie.FindAsync(id);
            if (subcategorie == null)
            {
                return NotFound();
            }
            ViewData["CategorieId"] = new SelectList(_context.Categorie, "Id", "Id", subcategorie.CategorieId);
            return View(subcategorie);
        }

        // POST: Subcategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naam,CategorieId")] Subcategorie subcategorie)
        {
            if (id != subcategorie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subcategorie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubcategorieExists(subcategorie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategorieId"] = new SelectList(_context.Categorie, "Id", "Id", subcategorie.CategorieId);
            return View(subcategorie);
        }

        // GET: Subcategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subcategorie = await _context.Subcategorie
                .Include(s => s.Categorie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subcategorie == null)
            {
                return NotFound();
            }

            return View(subcategorie);
        }

        // POST: Subcategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subcategorie = await _context.Subcategorie.FindAsync(id);
            _context.Subcategorie.Remove(subcategorie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubcategorieExists(int id)
        {
            return _context.Subcategorie.Any(e => e.Id == id);
        }
    }
}
