using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoMaMarkt.Models;
using CoMamarkt.Data;

namespace CoMamarkt.Controllers
{
    public class NieuwsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NieuwsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Nieuws
        public async Task<IActionResult> Index()
        {
            
            return View(await _context.Nieuws.ToListAsync());
            
        }

        // GET: Nieuws/Details/5
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

       
    }
}
