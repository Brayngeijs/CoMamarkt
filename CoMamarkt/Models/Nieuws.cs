using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoMaMarkt.Models
{
    public class Nieuws
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Bericht { get; set; }
        public DateTime Datum { get; set; }
        public string Image { get; set; }
    }
}
