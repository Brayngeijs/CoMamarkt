using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CoMamarkt.Areas.CMS.Controllers
{
    public class CMSController : Controller
    {
        [Area("CMS")]
        public IActionResult Index()
        {
            return View();
        }
    }
}