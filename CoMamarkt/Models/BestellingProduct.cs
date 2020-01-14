using CoMaMarkt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoMamarkt.Models
{
    public class BestellingProduct
    {
        public int Id { get; set; }
        public Bestelling Bestelling { get; set; }
        public int BestellingId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }

    }
}
