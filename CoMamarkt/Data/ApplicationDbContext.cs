using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CoMaMarkt.Models;

namespace CoMamarkt.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CoMaMarkt.Models.Categorie> Categorie { get; set; }
        public DbSet<CoMaMarkt.Models.Subcategorie> Subcategorie { get; set; }
        public DbSet<CoMaMarkt.Models.Subsubcategorie> Subsubcategorie { get; set; }
        public DbSet<CoMaMarkt.Models.Product> Product { get; set; }
        public DbSet<CoMaMarkt.Models.Bezorgmoment> Bezorgmoment { get; set; }
        public DbSet<Bestelling> Bestelling { get; set; }
        
    }
}
