using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoMaMarkt.Models
{
    public class Categorie
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string BannerURL { get; set; }
        public List<Product> Products { get; set; }
        public List<Subcategorie> Subcategorieen { get; set; }
        public string Image { get; set; } 
    }
}
