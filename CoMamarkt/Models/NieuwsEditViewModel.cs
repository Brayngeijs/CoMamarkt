using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoMamarkt.Models
{
    public class NieuwsEditViewModel : NieuwsCreateViewModel
    {
        public int Id { get; set; }
        public string BetsaandeImageUrl { get; set; }
    }
}
