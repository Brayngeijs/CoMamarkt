using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoMamarkt.Models
{
    public class CategorieEditViewModel : CategorieViewModel
    {
        public int Id { get; set; }
        public string BestaandeBannerURL { get; set; }
        public string BestaandeImageURL { get; set; }
    }
}
