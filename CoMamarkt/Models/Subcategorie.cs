using System.Collections.Generic;

namespace CoMaMarkt.Models
{
    public class Subcategorie
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public List<Product> Products { get; set; }
        public Categorie Categorie { get; set; }
        public int? CategorieId { get; set; }
        public List<Subsubcategorie> Subsubcategorieen { get; set; }
    }
}
