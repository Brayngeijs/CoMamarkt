using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoMamarkt.Models
{
    public class User : IdentityUser
    {
        public string Voornaam { get; set; }
        public string Tussenvoegsel { get; set; }
        public string Achternaam { get; set; }
        public DateTime Geboortedatum { get; set; }
        public string Straat { get; set; }
        public string Huisnummer { get; set; }
        public string Plaats { get; set; }
        public string Postcode { get; set; }
        public string Telefoonnummer { get; set; }
    }
}
