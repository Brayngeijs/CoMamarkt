using CoMaMarkt.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoMamarkt.Models
{
    public class Checkout
    {
        public List<Winkelwagen> WagenItems { get; set; }
        public double Totaal { get; set; }
        public string Naam { get; set; }
        public string Woonplaats { get; set; }
        public string Straat { get; set; }
        public string Huisnummer { get; set; }
       
    }
}
