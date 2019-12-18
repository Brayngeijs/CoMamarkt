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
using CoMamarkt.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace CoMamarkt.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public readonly IHostingEnvironment hostingEnvironment;

        public CategoriesController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var subcategorieën = await _context.Categorie.Include(c => c.Subcategorieen).ToListAsync();
            return View(subcategorieën);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var categorie = await _context.Categorie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categorie == null)
            {
                return NotFound();
            }

            return View(categorie);
        }

        public IActionResult Subcategorieën(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var subcategorie = _context.Subcategorie.Where(m => m.CategorieId == id).Include(p => p.Products).Include(p => p.Categorie);
            if (subcategorie == null)
            {
                return NotFound();
            }
            return View(subcategorie);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naam")] Categorie categorie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categorie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var categorie = _context.Categorie.Find(id);
            CategorieEditViewModel categorieEditViewModel = new CategorieEditViewModel
            {
                Id = categorie.Id,
                Naam = categorie.Naam,
                BestaandeBannerURL = categorie.BannerURL,
                BestaandeImageURL = categorie.Image
            };

            return View(categorieEditViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> FormEdit(CategorieEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var categorie = _context.Categorie.Find(model.Id);
                categorie.Naam = model.Naam;
                if (model.UploadImage != null)
                {
                    if (model.BestaandeImageURL != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "Images", model.BestaandeImageURL);
                        System.IO.File.Delete(filePath);
                    }
                    categorie.Image = ProcessUploadedImageFile(model);

                }
                if (model.UploadBannerURL != null)
                {
                    if (model.BestaandeBannerURL != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "Images", model.BestaandeBannerURL);
                        System.IO.File.Delete(filePath);
                    }
                    categorie.BannerURL = ProcessUploadedBannerFile(model);

                }
                _context.Update(categorie);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }



            return View(model);

        }

        private string ProcessUploadedImageFile(CategorieViewModel model)
        {
            string uniekImageNaam = null;
            if (model.UploadImage != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Images");
                uniekImageNaam = Guid.NewGuid().ToString() + "_" + model.UploadImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniekImageNaam);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.UploadImage.CopyTo(fileStream);
                }
            }

            return uniekImageNaam;
        }
        private string ProcessUploadedBannerFile(CategorieViewModel model)
        {
            string uniekBannerNaam = null;
            if (model.UploadBannerURL != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Images");
                uniekBannerNaam = Guid.NewGuid().ToString() + "_" + model.UploadBannerURL.FileName;
                string filePath = Path.Combine(uploadsFolder, uniekBannerNaam);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.UploadBannerURL.CopyTo(fileStream);
                }
            }

            return uniekBannerNaam;
        }
        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naam")] Categorie categorie)
        {
            if (id != categorie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categorie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategorieExists(categorie.Id))
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
            return View(categorie);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorie = await _context.Categorie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categorie == null)
            {
                return NotFound();
            }

            return View(categorie);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categorie = await _context.Categorie.FindAsync(id);
            _context.Categorie.Remove(categorie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategorieExists(int id)
        {
            return _context.Categorie.Any(e => e.Id == id);
        }
    }
}
