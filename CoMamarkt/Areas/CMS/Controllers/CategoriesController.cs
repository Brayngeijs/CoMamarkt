using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoMamarkt.Data;
using CoMamarkt.Models;
using CoMaMarkt.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoMamarkt.Areas.CMS.Controllers
{
    [Area("CMS")]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public readonly IWebHostEnvironment webHostEnvironment;

        public CategoriesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        { 
            var subcategorieën = await _context.Categorie.Include(c => c.Subcategorieen).ToListAsync();
            return View(subcategorieën);
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
        public async Task<IActionResult> Edit(CategorieEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var categorie = _context.Categorie.Find(model.Id);
                categorie.Naam = model.Naam;
                if (model.UploadImage != null)
                {
                    if (model.BestaandeImageURL != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "Images", model.BestaandeImageURL);
                        System.IO.File.Delete(filePath);
                    }
                    categorie.Image = ProcessUploadedImageFile(model);

                }
                if (model.UploadBannerURL != null)
                {
                    if (model.BestaandeBannerURL != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "Images", model.BestaandeBannerURL);
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
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images");
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
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images");
                uniekBannerNaam = Guid.NewGuid().ToString() + "_" + model.UploadBannerURL.FileName;
                string filePath = Path.Combine(uploadsFolder, uniekBannerNaam);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.UploadBannerURL.CopyTo(fileStream);
                }
            }

            return uniekBannerNaam;
        }
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
    }
}