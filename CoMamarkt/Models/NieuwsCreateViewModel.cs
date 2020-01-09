using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoMamarkt.Models
{
    public class NieuwsCreateViewModel
    {
        public string Titel { get; set; }
        public string Bericht { get; set; }
        public DateTime Datum { get; set; }
        public IFormFile UploadImage { get; set; }
        public NieuwsCreateViewModel()
        {
            Datum = DateTime.Now;
        }
    }
}
