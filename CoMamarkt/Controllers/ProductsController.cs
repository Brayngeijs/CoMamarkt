﻿using System;
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

namespace CoMamarkt.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Product.Include(p => p.Categorie).Include(p => p.Subcategorie).Include(p => p.Subsubcategorie);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Categorie)
                .Include(p => p.Subcategorie)
                .Include(p => p.Subsubcategorie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategorieNaam"] = new SelectList(_context.Categorie, "Id", "Naam");
            ViewData["SubcategorieNaam"] = new SelectList(_context.Subcategorie, "Id", "Naam");
            ViewData["SubsubcategorieNaam"] = new SelectList(_context.Subsubcategorie, "Id", "Naam");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategorieId,SubcategorieId,SubsubcategorieId,Id,EAN,Naam,Merk,KorteOmschrijving,Omschrijving,Image,Gewicht,Prijs")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategorieId"] = new SelectList(_context.Categorie, "Id", "Id", product.CategorieId);
            ViewData["SubcategorieId"] = new SelectList(_context.Subcategorie, "Id", "Id", product.SubcategorieId);
            ViewData["SubsubcategorieId"] = new SelectList(_context.Subsubcategorie, "Id", "Id", product.SubsubcategorieId);
            return View(product);
        }

         public async Task<IActionResult> LoadXml() 
        {
            XmlDocument xdoc = new XmlDocument();

            xdoc.Load(
                "https://supermaco.starwave.nl/api/products"
                );

            XmlNodeList elemList = xdoc.GetElementsByTagName("Product");

            for (int i = 0; i < elemList.Count; i++)
            {
                Product p = new Product();
                p.EAN = Convert.ToInt64(elemList[i].SelectSingleNode("./EAN").InnerXml);
                p.Naam = elemList[i].SelectSingleNode("./Title").InnerXml;
                p.Merk = elemList[i].SelectSingleNode("./Brand").InnerXml;
                p.KorteOmschrijving = elemList[i].SelectSingleNode("./Shortdescription").InnerXml;
                p.Omschrijving = elemList[i].SelectSingleNode("./Fulldescription").InnerXml;
                p.Image = elemList[i].SelectSingleNode("./Image").InnerXml;
                p.Gewicht = elemList[i].SelectSingleNode("./Weight").InnerXml;
                p.Prijs = Convert.ToDouble(elemList[i].SelectSingleNode("./Price").InnerXml, CultureInfo.InvariantCulture);
                //p.Categorie = elemList[i].SelectSingleNode("./Category").InnerXml;
                _context.Add(p);
            }

           
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategorieId"] = new SelectList(_context.Categorie, "Id", "Id", product.CategorieId);
            ViewData["SubcategorieId"] = new SelectList(_context.Subcategorie, "Id", "Id", product.SubcategorieId);
            ViewData["SubsubcategorieId"] = new SelectList(_context.Subsubcategorie, "Id", "Id", product.SubsubcategorieId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategorieId,SubcategorieId,SubsubcategorieId,Id,EAN,Naam,Merk,KorteOmschrijving,Omschrijving,Image,Gewicht,Prijs")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["CategorieId"] = new SelectList(_context.Categorie, "Id", "Id", product.CategorieId);
            ViewData["SubcategorieId"] = new SelectList(_context.Subcategorie, "Id", "Id", product.SubcategorieId);
            ViewData["SubsubcategorieId"] = new SelectList(_context.Subsubcategorie, "Id", "Id", product.SubsubcategorieId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Categorie)
                .Include(p => p.Subcategorie)
                .Include(p => p.Subsubcategorie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}