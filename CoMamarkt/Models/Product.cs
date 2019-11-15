using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoMaMarkt.Models
{
    public class Product
    {
        public int Id { get; set; }
        public long EAN { get; set; }
        public string Naam { get; set; }
        public string Merk { get; set; }
        public string KorteOmschrijving { get; set; }
        public string Omschrijving { get; set; }
        public string Image { get; set; }
        public string Gewicht { get; set; }
        public decimal Prijs { get; set; }

        public int CategorieId { get; set; }
        public Categorie Categorie { get; set; }

        public int SubcategorieId { get; set; }
        public Subcategorie Subcategorie { get; set; }

        public int SubsubcategorieId { get; set; }
        public Subsubcategorie Subsubcategorie { get; set; }
    }
}
