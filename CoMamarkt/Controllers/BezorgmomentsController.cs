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

namespace CoMamarkt.Controllers
{
    public class BezorgmomentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BezorgmomentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bezorgmoments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bezorgmoment.ToListAsync());
        }

        // GET: Bezorgmoments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bezorgmoment = await _context.Bezorgmoment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bezorgmoment == null)
            {
                return NotFound();
            }

            return View(bezorgmoment);
        }

        // GET: Bezorgmoments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bezorgmoments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Datum,BeginTijd,EindTijd,Prijs")] Bezorgmoment bezorgmoment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bezorgmoment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bezorgmoment);
        }
        
        public async Task<IActionResult> LoadXml()
        {
            XmlDocument xdoc = new XmlDocument();

            xdoc.Load("https://supermaco.starwave.nl/api/deliveryslots");

            XmlNodeList elemList = xdoc.GetElementsByTagName("Deliveryslot");
            for (int i = 0; i < elemList.Count; i++)
            {
                XmlNodeList Timeslots = elemList[i].SelectNodes("./Timeslots");
                for (int y = 0; y < Timeslots.Count; y++)
                {
                    XmlNodeList Timeslot = Timeslots[y].SelectNodes("./Timeslot");
                    for (int x = 0; x < Timeslot.Count; x++)
                    {
                        Bezorgmoment b = new Bezorgmoment();
                        b.Datum = Convert.ToDateTime(elemList[i].SelectSingleNode("./Date").InnerXml);
                        b.BeginTijd = Convert.ToDateTime(Timeslot[x].SelectSingleNode("./StartTime").InnerXml);
                        b.EindTijd = Convert.ToDateTime(Timeslot[x].SelectSingleNode("./EndTime").InnerXml);
                        b.Prijs = Convert.ToDouble(Timeslot[x].SelectSingleNode("./Price").InnerXml, CultureInfo.InvariantCulture);
                        _context.Add(b);

                              
                    }
                }
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        
        


        // GET: Bezorgmoments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bezorgmoment = await _context.Bezorgmoment.FindAsync(id);
            if (bezorgmoment == null)
            {
                return NotFound();
            }
            return View(bezorgmoment);
        }

        // POST: Bezorgmoments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Datum,BeginTijd,EindTijd,Prijs")] Bezorgmoment bezorgmoment)
        {
            if (id != bezorgmoment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bezorgmoment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BezorgmomentExists(bezorgmoment.Id))
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
            return View(bezorgmoment);
        }

        // GET: Bezorgmoments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bezorgmoment = await _context.Bezorgmoment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bezorgmoment == null)
            {
                return NotFound();
            }

            return View(bezorgmoment);
        }

        // POST: Bezorgmoments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bezorgmoment = await _context.Bezorgmoment.FindAsync(id);
            _context.Bezorgmoment.Remove(bezorgmoment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BezorgmomentExists(int id)
        {
            return _context.Bezorgmoment.Any(e => e.Id == id);
        }
    }

}
