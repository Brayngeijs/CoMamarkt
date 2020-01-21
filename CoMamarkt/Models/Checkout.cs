using CoMaMarkt.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoMamarkt.Models
{
    public class Checkout
    {
        public List<Winkelwagen> WagenItems { get; set; }
        public double TotalPrice { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
    }
}
