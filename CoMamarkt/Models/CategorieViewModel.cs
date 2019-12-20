using CoMaMarkt.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoMamarkt.Models
{
    public class CategorieViewModel
    {
        public string Naam { get; set; }
        public IFormFile UploadBannerURL { get; set; }
        public IFormFile UploadImage { get; set; }
    }
}
