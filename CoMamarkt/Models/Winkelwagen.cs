using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoMaMarkt.Models
{
    public class Winkelwagen
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public double Prijs { get; set; }
        public string Naam { get; set; }
        public string Image { get; set; }
        public Product Product { get; set; }
        public double Totaal
        {
            get
            {
                return Prijs * Amount;
            }
        }
        
    }
}
