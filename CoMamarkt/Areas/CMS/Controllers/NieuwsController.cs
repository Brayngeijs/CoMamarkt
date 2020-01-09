using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoMaMarkt.Models;
using CoMamarkt.Data;
using CoMamarkt.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace CoMamarkt.Areas.CMS.Controllers
{
    [Area("CMS")]
    public class NieuwsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public readonly IWebHostEnvironment webHostEnvironment;

        public NieuwsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: CMS/Nieuws
        public async Task<IActionResult> Index()
        {
            return View(await _context.Nieuws.ToListAsync());
        }

        // GET: CMS/Nieuws/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nieuws = await _context.Nieuws
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nieuws == null)
            {
                return NotFound();
            }

            return View(nieuws);
        }

        // GET: CMS/Nieuws/Create
        public IActionResult Create()
        {
            var model = new NieuwsCreateViewModel { Datum = DateTime.Now };
            return View(model);
        }

        // POST: CMS/Nieuws/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titel,Bericht,Datum,UploadImage")] NieuwsCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniekeBestandNaam = null;
                if (model.UploadImage != null)
                {
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "ImagesNieuws");
                    uniekeBestandNaam = Guid.NewGuid().ToString() + "_" + model.UploadImage.FileName;
                    string bestandLoacatie = Path.Combine(uploadsFolder, uniekeBestandNaam);
                    model.UploadImage.CopyTo(new FileStream(bestandLoacatie, FileMode.Create));
                }
                Nieuws nieuwNieuws = new Nieuws
                {
                    Titel = model.Titel,
                    Bericht = model.Bericht,
                    Datum = model.Datum,
                    Image = uniekeBestandNaam
                };
                _context.Add(nieuwNieuws);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: CMS/Nieuws/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nieuws = await _context.Nieuws.FindAsync(id);
            if (nieuws == null)
            {
                return NotFound();
            }
            return View(nieuws);
        }

        // POST: CMS/Nieuws/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titel,Bericht,Datum,Image")] Nieuws nieuws)
        {
            if (id != nieuws.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nieuws);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NieuwsExists(nieuws.Id))
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
            return View(nieuws);
        }

        // GET: CMS/Nieuws/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nieuws = await _context.Nieuws
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nieuws == null)
            {
                return NotFound();
            }

            return View(nieuws);
        }

        // POST: CMS/Nieuws/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nieuws = await _context.Nieuws.FindAsync(id);
            _context.Nieuws.Remove(nieuws);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NieuwsExists(int id)
        {
            return _context.Nieuws.Any(e => e.Id == id);
        }
    }
}
