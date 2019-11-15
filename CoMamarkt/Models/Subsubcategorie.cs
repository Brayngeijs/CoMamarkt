using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoMaMarkt.Models
{
    public class Subsubcategorie
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public List<Product> Products { get; set; }
        public Subcategorie Subcategorie { get; set; }
        public int? SubcategorieId { get; set; }
    }
}
