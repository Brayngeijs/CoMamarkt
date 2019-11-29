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
    public class SubsubcategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubsubcategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Subsubcategories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Subsubcategorie.Include(s => s.Subcategorie);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Subsubcategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subsubcategorie = await _context.Subsubcategorie
                .Include(s => s.Subcategorie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subsubcategorie == null)
            {
                return NotFound();
            }

            return View(subsubcategorie);
        }

        // GET: Subsubcategories/Create
        public IActionResult Create()
        {
            ViewData["SubcategorieNaam"] = new SelectList(_context.Subcategorie, "Id", "Naam");
            return View();
        }

        // POST: Subsubcategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naam,SubcategorieId")] Subsubcategorie subsubcategorie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subsubcategorie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubcategorieId"] = new SelectList(_context.Subcategorie, "Id", "Id", subsubcategorie.SubcategorieId);
            return View(subsubcategorie);
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
                    XmlNodeList subsubCategories = subCategories[y].SelectNodes("./Subsubcategory");
                    for (int x = 0; x < subsubCategories.Count; x++)
                    {
                        Subsubcategorie ssc = new Subsubcategorie();
                        ssc.Naam = subsubCategories[x].SelectSingleNode("./Name").InnerXml;


                        _context.Add(ssc);
                    }
                }
            }


            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Subsubcategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subsubcategorie = await _context.Subsubcategorie.FindAsync(id);
            if (subsubcategorie == null)
            {
                return NotFound();
            }
            ViewData["SubcategorieId"] = new SelectList(_context.Subcategorie, "Id", "Id", subsubcategorie.SubcategorieId);
            return View(subsubcategorie);
        }

        // POST: Subsubcategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naam,SubcategorieId")] Subsubcategorie subsubcategorie)
        {
            if (id != subsubcategorie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subsubcategorie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubsubcategorieExists(subsubcategorie.Id))
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
            ViewData["SubcategorieId"] = new SelectList(_context.Subcategorie, "Id", "Id", subsubcategorie.SubcategorieId);
            return View(subsubcategorie);
        }

        // GET: Subsubcategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subsubcategorie = await _context.Subsubcategorie
                .Include(s => s.Subcategorie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subsubcategorie == null)
            {
                return NotFound();
            }

            return View(subsubcategorie);
        }

        // POST: Subsubcategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subsubcategorie = await _context.Subsubcategorie.FindAsync(id);
            _context.Subsubcategorie.Remove(subsubcategorie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubsubcategorieExists(int id)
        {
            return _context.Subsubcategorie.Any(e => e.Id == id);
        }
    }
}
