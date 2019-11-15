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
        public TimeSpan BeginTijd { get; set; }
        public TimeSpan EindTijd { get; set; }
        public double Prijs { get; set; }
    }
}
