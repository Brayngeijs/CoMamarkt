using CoMamarkt.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoMamarkt
{
    public class Seed
    {
        public static void SeedUsers(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (roleManager.FindByNameAsync("admin").Result == null)
            {
                IdentityResult result = roleManager.CreateAsync(new IdentityRole("Admin")).Result;
                if (!result.Succeeded)
                {
                    throw new Exception("Rol niet aangemaakt");
                }
            }
            if (userManager.FindByEmailAsync("abc@xyz.com").Result == null)
            {
                User user = new User
                {
                    Voornaam = "Admin",
                    Achternaam = "Jansen",
                    Straat = "Adminstraat",
                    Huisnummer = "69",
                    Plaats = "Adminstad",
                    Postcode = "1234AB",
                    UserName = "abc@xyz.com",
                    Email = "abc@xyz.com",
                    EmailConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync(user, "Welkom01!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
            //Webredacteur
            if (roleManager.FindByNameAsync("webredacteur").Result == null)
            {
                IdentityResult result = roleManager.CreateAsync(new IdentityRole("Webredacteur")).Result;
                if (!result.Succeeded)
                {
                    throw new Exception("Rol niet aangemaakt");
                }
            }
            if (userManager.FindByEmailAsync("web@coma.com").Result == null)
            {
                User user = new User
                {
                    Voornaam = "Webredacteur",
                    Achternaam = "Jansen",
                    Straat = "Webredacteurstraat",
                    Huisnummer = "69",
                    Plaats = "Webredacteurstad",
                    Postcode = "1234AB",
                    UserName = "web@coma.com",
                    Email = "web@coma.com",
                    EmailConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync(user, "Welkom01!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Webredacteur").Wait();
                }
            }

        }
    }
}
