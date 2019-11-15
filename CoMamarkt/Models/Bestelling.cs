using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoMaMarkt.Models
{
    public class Bestelling
    {

        public int Id { get; set; }
        public string Titel { get; set; }
        public long EAN { get; set; }
        public double KortingsPrijs { get; set; }
        public DateTime GeldigTot { get; set; }

    }
}
