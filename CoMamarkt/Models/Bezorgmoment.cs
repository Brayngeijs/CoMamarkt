using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoMaMarkt.Models
{
    public class Bezorgmoment
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public DateTime BeginTijd { get; set; }
        public DateTime EindTijd { get; set; }
        public double Prijs { get; set; }
    }
}
