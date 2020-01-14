using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoMaMarkt.Models
{
    public class Bestelling
    {

        public int Id { get; set; }
        public string Naam { get; set; }
        public string Woonplaats { get; set; }
        public string Straat { get; set; }
        public string Huisnummer { get; set; }
        public DateTime BestellingDatum { get; set; }
        [NotMapped]
        public List<Product> Products { get; set; }
        public IdentityUser User { get; set; }
        public string UserId { get; set; }
        public Bestelling()
        {
            BestellingDatum = DateTime.Now;
        }

    }
}
